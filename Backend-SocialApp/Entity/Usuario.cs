using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Entity
{
    public class Usuario
    {
        [Key]
        [Column(TypeName = "varchar(40)")]
        public string Correo { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Nombres { get; set; }
        [Column(TypeName  = "varchar(25)")]
        public string Apellidos { get; set; }
        [Column(TypeName = "varchar(9)")]
        public string Sexo { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string KeyPasswordDesEncriptar { get; set; }
        public byte[] ImagePerfil { get; set; }
        
    }
}
