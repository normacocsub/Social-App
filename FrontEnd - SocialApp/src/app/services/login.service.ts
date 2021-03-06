import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Usuario } from '../models/usuario';
import { map, tap, catchError } from 'rxjs/operators';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { Platform } from '@ionic/angular';
import { FileTransfer, FileUploadOptions, FileTransferObject } from '@ionic-native/file-transfer/ngx';
import { BehaviorSubject, of } from 'rxjs';
import { environment } from 'src/environments/environment';

const ruta = environment.ruta;

@Injectable({
  providedIn: 'root'
})



export class LoginService {

  ruta: string = "";
  sqlObject: SQLiteObject;
  private currentUserSubject: BehaviorSubject<Usuario>;
  authSubject  =   new  BehaviorSubject(false);
  usuario: Usuario = new Usuario();
  constructor(private platform: Platform,
              private storage: Storage,
              private sqlite: SQLite,
              private http: HttpClient,
              private fileTransfer: FileTransfer) {
                this.ruta = ruta;
                this.storage.get("Login").then(val =>{
                  if(val != null){
                    this.authSubject.next(true);
                  }
                  else{
                    this.authSubject.next(false);
                  }
                })
              }


  post(usuario: Usuario){
    return this.http.post(this.ruta+"api/Usuario", usuario);
  }
  
  async logout() {
    await this.storage.remove('Login');
    this.authSubject.next(false);
  }


  loguearse(user: string, password: string){
    
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    var usuario = new Usuario();
    usuario.correo = user;
    usuario.password = password;
    return this.http.post<Usuario>(this.ruta+"api/Usuario/login", usuario ,  httpOptions).pipe(
      tap(
        async(result: Usuario) => {
          if(result){
            console.log(result.imagePerfil);
            await this.storage.set('Login', result);
            this.authSubject.next(true);
          }
        }
      )
    )
  }

  async buscarUsuario(correo: string) {
    const headers = await this.headersToken();
    return this.http.get<Usuario>(this.ruta+"api/Usuario/usuario/"+correo, {"headers": headers});
  }

  async headersToken() {
    var token = '';
    await this.getUser().then((value) => {
      value.subscribe((result:Usuario) => {
        token = result.token || '';
      });
    });
    const headers = { 'content-type': 'application/json', "authorization": `Bearer ${token}`}  
    return headers;
  }

  async editarImagen(usuario: Usuario){
    const headers = await this.headersToken();
    return this.http.put<Usuario>(this.ruta + "api/Usuario/Imagen",usuario, {"headers": headers})
    .pipe(
      tap(
        async(result: Usuario) => {
          if(result){
            await this.storage.set('Login', result);
            this.authSubject.next(true);
          }
        }
      )
    )
  }

  async cargarUsuario(){
    const usuario = await this.storage.get('Login');
    if(usuario){
      console.log(usuario);
      this.currentUserSubject = new BehaviorSubject<Usuario>(usuario);
    }
    return;
  }

  isLoggedIn() {
    return this.authSubject.asObservable();
  }

  async getUser(){
    await this.cargarUsuario();
    return this.currentUserSubject.asObservable();
  }


  

}
