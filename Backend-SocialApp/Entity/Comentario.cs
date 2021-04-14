using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Comentario
    {
        [Key]
        public string IdComentario { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string ContenidoComentario { get; set; }
        public string PublicacionId { get; set; }
        [NotMapped]
        public Usuario Usuario { get; set; }
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}