using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Publicacion
    {
        [Key]
        public string IdPublicacion { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Nombre { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string ContenidoPublicacion { get; set; }
        public string Imagen { get; set; } 
        public List<Comentario> Comentarios { get; set; }

        public Publicacion()
        {
            this.Comentarios = new List<Comentario>();
        }

        public void AgregarComentarios(Comentario comentario)
        {
            comentario.IdComentario = Seguridad.RandomString(16);
            this.Comentarios.Add(comentario);
        }
    }
}