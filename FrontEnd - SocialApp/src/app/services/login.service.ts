import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Usuario } from '../models/usuario';
import { map, tap, catchError } from 'rxjs/operators';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { Platform } from '@ionic/angular';
import { FileTransfer, FileUploadOptions, FileTransferObject } from '@ionic-native/file-transfer/ngx';
import { BehaviorSubject, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})



export class LoginService {

  ruta: string ="https://socialapiapp.azurewebsites.net/"
  sqlObject: SQLiteObject;
  private currentUserSubject: BehaviorSubject<Usuario>;
  authSubject  =   new  BehaviorSubject(false);
  usuario: Usuario = new Usuario();
  constructor(private platform: Platform,
              private storage: Storage,
              private sqlite: SQLite,
              private http: HttpClient,
              private fileTransfer: FileTransfer) {
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
            await this.storage.set('Login', result);
            this.authSubject.next(true);
          }
        }
      )
    )
  }

  isLoggedIn() {
    return this.authSubject.asObservable();
  }


  

}
