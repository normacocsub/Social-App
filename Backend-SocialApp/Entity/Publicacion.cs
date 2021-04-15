using System;
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
        [NotMapped]
        public Usuario Usuario { get; set; }
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        [NotMapped]
        public Reaccion Reaccion { get; set; }
        public List<Reaccion> Reacciones { get; set; }

        public Publicacion()
        {
            this.Comentarios = new List<Comentario>();
            this.Reacciones = new List<Reaccion>();
        }

        public void agregarReaccion(Reaccion reaccion)
        {
            Reaccion = reaccion;

            var response = Reacciones.Find(r =>  r.IdUsuario == Reaccion.IdUsuario);
            if(response != null)
            {
                Reacciones.Remove(response);
                response.Like = Reaccion.Like;
                response.Love = Reaccion.Love;
                Reacciones.Add(response);
            }
            else
            {
                Reaccion.CrearCodigo();
                Reacciones.Add(Reaccion);
            }
        }


    }
}