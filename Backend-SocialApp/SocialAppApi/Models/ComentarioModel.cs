using System;
using Entity;

namespace SocialAppApi.Models
{
    public class ComentarioInputModel
    {
        public string IdComentario { get; set; }
        public string ContenidoComentario { get; set; }
        public string PublicacionId { get; set; }
        public Usuario Usuario { get; set; }
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ComentarioViewModel : ComentarioInputModel
    {
        public ComentarioViewModel() {}

        public ComentarioViewModel(Comentario comentario)
        {
            IdComentario = comentario.IdComentario;
            ContenidoComentario = comentario.ContenidoComentario;
            PublicacionId = comentario.PublicacionId;
            Usuario = comentario.Usuario;
            IdUsuario = comentario.IdUsuario;
            Fecha = comentario.Fecha;
        }
    }
}