using System;
using Datos;
using Entity;

namespace Logica
{
    public class UsuarioService
    {
        private SocialAppContext _context;

        public UsuarioService(SocialAppContext context)
        {
            _context = context;
        }


        public GuardarUsuarioResponse GuardarUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return new GuardarUsuarioResponse(usuario);
            }
            catch(Exception e)
            {
                return new GuardarUsuarioResponse($"Error en la aplicacion: {e.Message}");
            }
        }

        public ValidarUsuarioResponse ValidarUsuario(Usuario usuario)
        {
            try
            {
                var response = _context.Usuarios.Find(usuario.Correo);
                if(response != null)
                {
                    var passwordDes = Seguridad.DesEncriptar(response.Password, response.KeyPasswordDesEncriptar);
                    usuario.Password = Seguridad.DesEncriptar(usuario.Password, usuario.KeyPasswordDesEncriptar);
                    if(passwordDes != usuario.Password)
                    {
                        return new ValidarUsuarioResponse("Error", "Invalid");
                    }
                    else
                    {
                        return new ValidarUsuarioResponse(response);
                    }
                }
                else
                {
                    return new ValidarUsuarioResponse("Error", "No Existe");
                }
            }
            catch(Exception e)
            {
                return new ValidarUsuarioResponse($"Error {e.Message}", "Aplication");
            }
        }


        public class ValidarUsuarioResponse
        {
            public ValidarUsuarioResponse(Usuario usuario)
            {
                Error = false;
                Usuario = usuario;
            }
            public ValidarUsuarioResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public string Estado { get; set; }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Usuario Usuario { get; set; }
        }

        public class GuardarUsuarioResponse
        {
            public GuardarUsuarioResponse(Usuario usuario)
            {
                Error = false;
                Usuario = usuario;
            }

            public GuardarUsuarioResponse(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }
            public Usuario Usuario { get; set; }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
        }
    }
}
