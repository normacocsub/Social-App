import { Comentario } from "./comentario";
import { Reaccion } from "./reaccion";
import { Usuario } from "./usuario";

export class Publicacion {
    idPublicacion: string;
    nombre: string;
    contenidoPublicacion: string;
    imagen: string;
    comentarios: Comentario[];
    usuario: Usuario;
    fecha: Date;
    reacciones: Reaccion[];

    constructor(){
        this.usuario = new Usuario();
        this.reacciones = [];
        this.comentarios = [];
    }

    
}
