using System.Collections.Generic;
using Entity;

namespace SocialAppApi.Models
{
    public class PublicacionInputModel
    {
        public string IdPublicacion { get; set; }
        public string Nombre { get; set; }
        public string ContenidoPublicacion { get; set; }
        public string Imagen { get; set; } 
        public List<Comentario> Comentarios { get; set; }
    }

    public class PublicacionViewModel : PublicacionInputModel 
    {
        public PublicacionViewModel(){}
        public PublicacionViewModel(Publicacion publicacion)
        {
            IdPublicacion = publicacion.IdPublicacion;
            Nombre = publicacion.Nombre;
            ContenidoPublicacion = publicacion.ContenidoPublicacion;
            Imagen = publicacion.Imagen;
            Comentarios = publicacion.Comentarios;
        }
    }
}