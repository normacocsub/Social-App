import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Comentar, Publicacion } from '../interfaces/interfaces';
import { Storage } from '@ionic/storage';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { Platform } from '@ionic/angular';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Usuario } from '../models/usuario';



@Injectable({
  providedIn: 'root'
})
export class PublicacionesService {
  publicaciones: Publicacion[] = [];
  comentarios: Comentar[] = [];
  private storag: SQLiteObject;
  ruta: string ="https://localhost:5001/"
  constructor(private platform: Platform,
              private storage: Storage,
               private sqlite: SQLite,
               private http: HttpClient
               ) { 
    this.openDataBase();
  }



  //Publicacion por api

  crearPublicacion(publicacion: Publicacion){
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    publicacion.comentarios = [];
    return this.http.post(this.ruta+"api/Publicacion", publicacion, httpOptions);
  }

  ConsultaPublicaciones(){
    return this.http.get<Publicacion[]>(this.ruta+"api/Publicacion");
  }

  editarPublicacion(publicacion: Publicacion){
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put("https://localhost:5001/api/Publicacion", publicacion, httpOptions);
  }

  agregarComentario(publicacion: Publicacion){
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put("https://localhost:5001/api/Publicacion/Comentarios", publicacion, httpOptions);
  }


  eliminarPublicacion(publicacion: Publicacion){
    return this.http.delete("https://localhost:5001/api/Publicacion/"+ publicacion.idPublicacion);
  }

  editarComentario(comentario: Comentar){
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    console.log(comentario.contenidoComentario);
    return this.http.put(this.ruta + "api/publicacion/EditarComentario", comentario, httpOptions);
  }


  //Publicacion Local


  

  async cargarPublicaciones(){
    const publicaciones = await this.storage.get('publicaciones');
    if(publicaciones){
      this.publicaciones = publicaciones;
    }
    return;
  }

   getPublicaciones(sql: SQLiteObject){
    this.publicaciones = [];
    sql.executeSql('SELECT * FROM Publicaciones', []).then(r =>{
      console.log(r.rows.length);
      if(r.rows.length > 0){
        for( var i = 0; i < r.rows.length; i++){
          var comment
          if(r.rows.item(i).comentarios != ''){
            comment = JSON.parse(r.rows.item(i).comentarios)
          }
          else{
            comment = [];
          }
          var publicacion = {
            idPublicacion: r.rows.item(i).idPublicacion,
            nombre: r.rows.item(i).nombre,
            contenidoPublicacion: r.rows.item(i).publicacion,
            imagen: r.rows.item(i).urlImg,
            comentarios: comment,
            usuario: new Usuario()
          }
          this.publicaciones.unshift(publicacion);
        }
      }
    });
  }


  //conect to sql Lite

  insertPublicaciones(publicacion: Publicacion){
    publicacion.idPublicacion = (this.publicaciones.length + 1).toString();
    publicacion.comentarios = [];
    let data = [ publicacion.idPublicacion, publicacion.nombre, publicacion.contenidoPublicacion, publicacion.imagen, JSON.stringify(publicacion.comentarios) ];
    this.storag.transaction(tx =>{
      tx.executeSql('INSERT INTO Publicaciones(idPublicacion,nombre,publicacion,urlImg, comentarios) VALUES(?,?,?,?,?)', data)
    });

    this.getPublicaciones(this.storag);
    return of(publicacion);
  }


  updatePublicacion(publicacion: Publicacion){
    let data = [ publicacion.contenidoPublicacion ];

    this.storag.transaction(tx => {
      tx.executeSql(`UPDATE Publicaciones SET publicacion=? WHERE idPublicacion=${publicacion.idPublicacion}`, data)
    });

    this.getPublicaciones(this.storag);
    return of(publicacion);
  }

  publicarComentario(publicacion: Publicacion){
    let data = [ JSON.stringify(publicacion.comentarios)];
    this.storag.transaction(tx => {
      tx.executeSql(`UPDATE Publicaciones SET comentarios=? WHERE idPublicacion=${publicacion.idPublicacion}`, data)
    });
    this.getPublicaciones(this.storag);
    return of(publicacion);
  }

  deletePublicacion(publicacion: Publicacion){
    this.storag.transaction(tx =>{
      tx.executeSql('DELETE FROM Publicaciones WHERE idPublicacion = ?', [publicacion.idPublicacion])
    });
    this.getPublicaciones(this.storag);
    return of(publicacion);
  }

  openDataBase(){
    this.platform.ready().then(() =>{
      this.sqlite.create({
        name: 'data.db',
        location: 'default',    
      }).then((db: SQLiteObject) => {
        db.executeSql('CREATE TABLE if not exists Publicaciones (idPublicacion varchar(5) primary key,nombre varchar(30), publicacion varchar(500), urlImg varchar(100), comentarios varchar(500))', [])
        .then(() => console.log('Executed SQL'))
        .catch(e => console.log(e));
        
        this.storag = db;
        this.getPublicaciones(db);
      })
      .catch(e => console.log(e));
    })
  }
}