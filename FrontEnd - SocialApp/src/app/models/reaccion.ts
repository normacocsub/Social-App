import { Usuario } from "./usuario";

export class Reaccion {
    codigo: string;
    like: boolean
    love: boolean;
    idUsuario: string;
    usuario: Usuario;
    idPublicacion: string;

    constructor(){
        this.usuario = new Usuario();
    }
}
