using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Reaccion
    {   
        [Key]
        public string Codigo { get; set; }
        public bool Like { get; set; }
        public bool Love { get; set; }
        public string IdUsuario { get; set; }
        [NotMapped]
        public Usuario Usuario { get; set; }
        [NotMapped]
        public string IdPublicacion { get; set; }


        public void CrearCodigo()
        {
            Codigo = Seguridad.RandomString(16);
        }

    }
}