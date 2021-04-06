export interface Publicacion {
  idPublicacion: string;
  nombre: string;
  contenidoPublicacion: string;
  imagen: string;
  comentarios: Comentar[];
}

export interface Comentar{
  idComentario: string;
  contenidoComentario: string;
  publicacionId: string;
}