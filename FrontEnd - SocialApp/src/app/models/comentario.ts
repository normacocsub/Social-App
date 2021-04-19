import { Usuario } from "./usuario";

export class Comentario {
    idComentario: string;
    contenidoComentario: string;
    publicacionId: string;
    usuario: Usuario;
    idUsuario: string;

    constructor(){
        this.usuario = new Usuario();
    }
}
