import { Comentario } from "./comentario";
import { Usuario } from "./usuario";

export class Publicacion {
    idPublicacion: string;
    nombre: string;
    contenidoPublicacion: string;
    imagen: string;
    comentarios: Comentario[];
    usuario: Usuario;

    constructor(){
        this.usuario = new Usuario();
    }
}
