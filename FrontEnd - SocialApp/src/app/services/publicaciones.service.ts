import { EventEmitter, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Publicacion } from '../models/publicacion';
import { Storage } from '@ionic/storage';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { Platform } from '@ionic/angular';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Usuario } from '../models/usuario';
import * as signalR from '@aspnet/signalr';
import { Comentario } from '../models/comentario';
import { environment } from 'src/environments/environment';
import { Reaccion } from '../models/reaccion';
import { LoginService } from './login.service';

const ruta = environment.ruta;

@Injectable({
  providedIn: 'root',
})
export class PublicacionesService {
  publicaciones: Publicacion[] = [];
  comentarios: Comentario[] = [];
  private storag: SQLiteObject;
  ruta: string = '';

  private hubConnection: signalR.HubConnection;
  signalRecived = new EventEmitter<Publicacion>();

  constructor(
    private platform: Platform,
    private storage: Storage,
    private sqlite: SQLite,
    private http: HttpClient,
    private serviceLogin: LoginService
  ) {
    this.ruta = ruta;
  }

  private buildConnection = () => {
    
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.ruta + 'signalHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
        
      })
      .build();

  };

  private startConnection = () => {
    console.log(this.hubConnection);
    this.hubConnection
      .start()
      .then(() => {
        console.log('Iniciando signal');
        this.registerSignalEvents();
      })
      .catch((err) => {
        console.log('Error en el signal ' + err);
        setTimeout(function () {
          this.startConnection();
        }, 3000);
      });
  };
  private registerSignalEvents() {
    this.hubConnection.on('publicacion', (data: Publicacion) => {
      this.signalRecived.emit(data);
    });
  }

  //Publicacion por api

  async crearPublicacion(publicacion: Publicacion) {
    
    const headers = await this.headersToken();
    publicacion.comentarios = [];
    publicacion.reacciones = [];
    return this.http.post(
      this.ruta + 'api/Publicacion',
      publicacion,
      {"headers" : headers}
    );
  }

  async ConsultaPublicaciones() {
    const headers = await this.headersToken();
    return this.http.get<Publicacion[]>(this.ruta + 'api/Publicacion', {"headers" : headers});
  }

  async editarPublicacion(publicacion: Publicacion) {
    const headers = await this.headersToken();
    return this.http.put(
      this.ruta + 'api/Publicacion',
      publicacion,
      {"headers" : headers}
    );
  }

  async agregarComentario(comentario: Comentario) {
    const headers = await this.headersToken();
    return this.http.put<Publicacion>(
      this.ruta + 'api/Publicacion/Comentarios',
      comentario,
      {"headers" : headers}
    );
  }

  async eliminarPublicacion(publicacion: Publicacion) {
    const headers = await this.headersToken();
    return this.http.delete(
      this.ruta + 'api/Publicacion/' + publicacion.idPublicacion, {"headers" : headers}
    );
  }

  async editarComentario(comentario: Comentario) {
    const headers = await this.headersToken();
    
    return this.http.put<Publicacion>(
      this.ruta + 'api/publicacion/EditarComentario',
      comentario,
      {"headers" : headers}
    );
  }

  async editarReaccion(reaccion: Reaccion){
    const headers = await this.headersToken();
    return this.http.put<Publicacion>(this.ruta + 'api/publicacion/Reaccion', reaccion, {"headers" : headers})
  }

  async consultarReacciones(){
    const headers = await this.headersToken();
    return this.http.get<Reaccion[]>(this.ruta + 'api/publicacion/', {"headers" : headers});
  }

  async eliminarReaccion(reaccion: string, idPublicacion: string){
    const headers = await this.headersToken();
    return this.http.delete<Publicacion>(this.ruta +"api/publicacion/Reaccion/"+reaccion
    +"/"+idPublicacion, {"headers" : headers});
  }

  async eliminarComentario(comentario: string, publicacion: string){
    const headers = await this.headersToken();
    return this.http.delete<Publicacion>(this.ruta+"api/publicacion/Comentario/"+comentario
    +"/"+publicacion, {"headers" : headers});
  }


  async headersToken() {
    var token = '';
    await this.serviceLogin.getUser().then((value) => {
      value.subscribe((result:Usuario) => {
        token = result.token || '';
      });
    });
    const headers = { 'content-type': 'application/json', "authorization": `Bearer ${token}`}  
    return headers;
  }

  //Publicacion Local

  async cargarPublicaciones() {
    const publicaciones = await this.storage.get('publicaciones');
    if (publicaciones) {
      this.publicaciones = publicaciones;
    }
    return;
  }

}
