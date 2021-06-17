using System;
using System.IO;
using Datos;
using Entity;
using Microsoft.AspNetCore.Hosting;

namespace Logica
{
    public class UsuarioService
    {
        private SocialAppContext _context;
        private IWebHostEnvironment Environment;

        public UsuarioService(SocialAppContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }


        public ClaseRespuesta<Usuario> GuardarUsuario(Usuario usuario)
        {
            try
            {
                string fileName = @"/ArchivosMultimedia/" + "/Perfil/ImagenBase64_" + Guid.NewGuid() + ".txt";
                FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Create, FileAccess.Write, FileShare.None);

                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(usuario.ImagePerfil);
                usuario.ImagePerfil = fileName;
                
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                writer.Close();
                fileStream.Close();
                return new ClaseRespuesta<Usuario>(usuario);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Usuario>($"Error en la aplicacion: {e.Message}");
            }
        }

        public ClaseRespuesta<Usuario> ValidarUsuario(Usuario usuario)
        {
            try
            {
                Usuario response = _context.Usuarios.Find(usuario.Correo);
                response = BuscarImagenPerfilUsuario(response);
                if (response == null)
                {
                    return new ClaseRespuesta<Usuario>("Error No Existe");
                }
                String passwordDes = Seguridad.DesEncriptar(response.Password, response.KeyPasswordDesEncriptar);
                usuario.Password = Seguridad.DesEncriptar(usuario.Password, usuario.KeyPasswordDesEncriptar);
                if(passwordDes != usuario.Password)
                {
                    return new ClaseRespuesta<Usuario>("Error Invalid");
                }
                return new ClaseRespuesta<Usuario>(response);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Usuario>($"Error {e.Message}");
            }
        }
        
        private Usuario BuscarImagenPerfilUsuario(Usuario usuario)
        {
            string fileName = usuario.ImagePerfil;
            if (fileName != "")
            {
                FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader reader = new StreamReader(fileStream);
                usuario.ImagePerfil = reader.ReadLine();
                    
                reader.Close();
                fileStream.Close();
            }

            return usuario;
        }

        public ClaseRespuesta<Usuario> EditarImagenUsuario(Usuario usuario)
        {
            try
            {
                Usuario result = _context.Usuarios.Find(usuario.Correo);
                if (result == null)
                {
                    return new ClaseRespuesta<Usuario>("No existe el usuario");
                }
                string fileName = result.ImagePerfil;
                fileName = fileName == ""
                    ? @"/ArchivosMultimedia/" + "/Perfil/ImagenBase64_" + Guid.NewGuid() + ".txt"
                    : fileName;
                FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(usuario.ImagePerfil);
                result.ImagePerfil = fileName;
                writer.Close();
                fileStream.Close();
                _context.Usuarios.Update(result);
                _context.SaveChanges();
                return new ClaseRespuesta<Usuario>(result);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Usuario>($"Error {e.Message}");
            }
        }

        public ClaseRespuesta<Usuario> BuscarUsuario(string correo)
        {
            try
            {
                Usuario response = _context.Usuarios.Find(correo);
                if (response == null)
                {
                    return new ClaseRespuesta<Usuario>("No existe el usuario");
                }
                string fileName = response.ImagePerfil;
                if (fileName != "")
                {
                    FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    StreamReader reader = new StreamReader(fileStream);
                    response.ImagePerfil = reader.ReadLine();
                    reader.Close();
                    fileStream.Close();
                }
                return new ClaseRespuesta<Usuario>(response);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Usuario>($"Error {e.Message}");
            }
        }

        
    }
}
