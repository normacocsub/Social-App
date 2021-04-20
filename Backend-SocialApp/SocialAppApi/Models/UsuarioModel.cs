using Entity;

namespace SocialAppApi.Models
{
    public class UsuarioInputModel
    {
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string ImagePerfil { get; set; }
    }

    public class UsuarioViewModel : UsuarioInputModel
    {
        public UsuarioViewModel(){}

        public UsuarioViewModel(Usuario usuario)
        {
            Correo = usuario.Correo;
            Password = usuario.Password;
            Nombres = usuario.Nombres;
            Apellidos = usuario.Apellidos;
            Sexo = usuario.Sexo;
            ImagePerfil = usuario.ImagePerfil;
        }
    }
}