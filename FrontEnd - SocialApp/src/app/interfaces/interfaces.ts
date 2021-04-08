import { Usuario } from "../models/usuario";

export interface Publicacion {
  idPublicacion: string;
  nombre: string;
  contenidoPublicacion: string;
  imagen: string;
  comentarios: Comentar[];
  usuario: Usuario;
}

export interface Comentar{
  idComentario: string;
  contenidoComentario: string;
  publicacionId: string;
  usuario: Usuario;
  idUsuario: string;
}