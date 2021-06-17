using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logica
{
    public class PublicacionService
    {
        private SocialAppContext _context;
        private IWebHostEnvironment Environment;
        
        public PublicacionService(SocialAppContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        public ClaseRespuesta<Publicacion> GuardarPublicacion(Publicacion publicacion)
        {
            try
            {
                publicacion.IdPublicacion = Seguridad.RandomString(16);
                string fileName = @"/ArchivosMultimedia/" + "ImagenBase64_" + Guid.NewGuid() + ".txt";
                FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(publicacion.Imagen);
                publicacion.Imagen = fileName;
                _context.Publicacions.Add(publicacion);
                _context.SaveChanges();
                writer.Close();
                fileStream.Close();
                return new ClaseRespuesta<Publicacion>(publicacion);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error aplicacion: {e.Message}");
            }
        }

        private Usuario BuscarImagenPerfilUsuario(string idUsuario)
        {
            Usuario usuario = _context.Usuarios.Find(idUsuario);
            Usuario returnUsuario = new Usuario();
            returnUsuario.Apellidos = usuario.Apellidos;
            returnUsuario.Correo = usuario.Correo;
            returnUsuario.Nombres = usuario.Nombres;
            returnUsuario.Sexo = usuario.Sexo;
            returnUsuario.ImagePerfil = usuario.ImagePerfil;
            string fileName = returnUsuario.ImagePerfil;
            if (fileName != "")
            {
                FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader reader = new StreamReader(fileStream);
                returnUsuario.ImagePerfil = reader.ReadLine();
                    
                reader.Close();
                fileStream.Close();
            }

            return returnUsuario;
        }

        public async Task<ClaseRespuesta<Publicacion>> ConsultarPublicaciones()
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(p => p.Comentarios.OrderByDescending(c => c.Fecha))
                .Include(p => p.Reacciones)
                .ToList().OrderByDescending(p => p.Fecha);
                foreach (var item in publicaciones)
                {
                    int usuarioResponse = publicaciones.ToList().Where(d => d.IdUsuario == item.IdUsuario).ToList().Count;
                    item.Usuario = BuscarImagenPerfilUsuario(item.IdUsuario);

                    item.Comentarios.ForEach(comentario =>
                    {
                        comentario.Usuario = BuscarImagenPerfilUsuario(comentario.IdUsuario);
                    });
                   
                    item.Reacciones.ForEach(reaccion =>
                    {
                        reaccion.Usuario = _context.Usuarios.Find(reaccion.IdUsuario);
                        reaccion.Usuario.ImagePerfil = "";
                    });

                    if (item.Imagen != "")
                    {
                        string fileName = item.Imagen;
                        FileStream fileStream = new FileStream( Environment.WebRootPath + fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                        StreamReader reader = new StreamReader(fileStream);
                        item.Imagen = reader.ReadLine();
                    
                        reader.Close();
                        fileStream.Close();
                    }
                }
                return new ClaseRespuesta<Publicacion>(_context.Publicacions.Include(p => p.Comentarios
                .OrderByDescending(c => c.Fecha)).ToList());
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error Aplication: {e.Message}");
            }
        }

        public ClaseRespuesta<Publicacion> EditarPublicaciones(Publicacion publicacion)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(c => c.Comentarios).ToList();
                var response = publicaciones.Find( p => p.IdPublicacion == publicacion.IdPublicacion);
                if (response == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe la publicacion ");
                }
                response.ContenidoPublicacion = publicacion.ContenidoPublicacion;
                _context.Publicacions.Update(response);
                _context.SaveChanges();
                return new ClaseRespuesta<Publicacion>(response);

            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion: {e.Message}");
            }
        }




        public ClaseRespuesta<Publicacion> AgregarComentarios(Comentario comentario)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(c => c.Comentarios)
                .ToList();
                var response = publicaciones.Find( p => p.IdPublicacion == comentario.IdPublicacion);
                if (response == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe la publicacion ");
                }
                response.Comentarios.Add(comentario);
                response.Comentarios.ForEach(comentario =>
                {
                    comentario.IdComentario = comentario.IdComentario == "" ? Seguridad.RandomString(16) : comentario.IdComentario;
                    comentario.Usuario = BuscarImagenPerfilUsuario(comentario.IdUsuario);
                });
                response.Usuario = BuscarImagenPerfilUsuario(response.IdUsuario);
                _context.Publicacions.Update(response);
                _context.SaveChanges();
                response.Comentarios = response.Comentarios.OrderByDescending(c => c.Fecha).ToList();
                return new ClaseRespuesta<Publicacion>(response);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion: {e.Message}");
            }
        }


        public ClaseRespuesta<Publicacion> EliminarPublicacion(string publicacion)
        {
            try
            {
                var response = _context.Publicacions.Find(publicacion);
                if (response == null)
                {
                    return new ClaseRespuesta<Publicacion>("Error: No existe la publicacion");
                }
                _context.Publicacions.Remove(response);
                _context.SaveChanges();
                return new ClaseRespuesta<Publicacion>(response);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {e.Message}");
            }
        }

        public async Task<ClaseRespuesta<Publicacion>> EditarComentario(Comentario comentario)
        {
            try
            {
                var comentarios = _context.Comentarios.ToList();
                var response = comentarios.Find(c => c.IdComentario == comentario.IdComentario);
                if (response == null)
                {
                    return new ClaseRespuesta<Publicacion>("No se encuentra el comentario");
                }
                response.Usuario = BuscarImagenPerfilUsuario(response.IdUsuario);
                response.ContenidoComentario = comentario.ContenidoComentario;
                if(response.Usuario.Correo != comentario.Usuario.Correo)
                {
                    return new ClaseRespuesta<Publicacion>("El usuario no puede editar este comentario");
                }
                _context.Comentarios.Update(response);
                _context.SaveChanges();
                
                var publicaciones = await ConsultarPublicaciones();
                if(publicaciones.Error)
                {
                    return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {publicaciones.Mensaje}");
                }
                var responsePublicacion = publicaciones.Lista.Find(c => c.IdPublicacion == comentario.IdPublicacion);
                responsePublicacion.Comentarios = responsePublicacion.Comentarios.OrderByDescending(c => c.Fecha).ToList();
                return new ClaseRespuesta<Publicacion>(responsePublicacion);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {e.Message}");
            }
        }

        public async Task<ClaseRespuesta<Publicacion>> EliminarComentario(string codigo, string publicacion)
        {
            try
            {
                var publicaciones = await ConsultarPublicaciones();
                if (publicaciones.Error)
                {
                    return new ClaseRespuesta<Publicacion>("Error al consultar las publicaciones");
                }
                Publicacion publicacionResponse = publicaciones.Lista.Find(p => p.IdPublicacion == publicacion);
                if (publicacionResponse == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe la publicacion");
                }
                Comentario respuestaComentario = publicacionResponse.Comentarios.Find(c => c.IdComentario == codigo);
                if (respuestaComentario == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe el comentario");
                }
                publicacionResponse.Comentarios.Remove(respuestaComentario);
                _context.Publicacions.Update(publicacionResponse);
                _context.SaveChanges();
                return new ClaseRespuesta<Publicacion>(publicacionResponse);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {e.Message}");
            }
        }

       

        public ClaseRespuesta<Publicacion> EditarReaccion(Reaccion reaccion)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(p => p.Comentarios)
                .Include(p => p.Reacciones)
                .ToList();
                Publicacion response = publicaciones.Find(p => p.IdPublicacion == reaccion.IdPublicacion);
                if (response == null)
                {
                    return new ClaseRespuesta<Publicacion>("No se encontro la publicacion");
                }
                if(reaccion.Like && reaccion.Love)
                {
                    return new ClaseRespuesta<Publicacion>("No se puede usar las dos reacciones a la vez");
                }
                if(!reaccion.Like && !reaccion.Love)
                {
                    return new ClaseRespuesta<Publicacion>("No se encuentra ninguna reaccion");
                }
                response.agregarReaccion(reaccion);
                _context.Publicacions.Update(response);
                _context.SaveChanges();
                response.Comentarios.ForEach(comentario =>
                {
                    comentario.Usuario = _context.Usuarios.Find(comentario.IdUsuario);
                    comentario.Usuario.ImagePerfil = "";
                });
                
                response.Reacciones.ForEach(reaccion =>
                {
                    reaccion.Usuario = _context.Usuarios.Find(reaccion.IdUsuario);
                    reaccion.Usuario.ImagePerfil = "";
                });
                response.Usuario = BuscarImagenPerfilUsuario(response.IdUsuario);
                return new ClaseRespuesta<Publicacion>(response);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {e.Message}");
            }
        }

        public async Task<ClaseRespuesta<Publicacion>> EliminarReaccion(string codigo, string IdPublicacion)
        {
            try
            {
                var response = await ConsultarPublicaciones();
                var reaccion = _context.Reacciones.Find(codigo);
                if (reaccion == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe la reaccion");
                }

                if (response.Error)
                {
                    return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {response.Mensaje}");
                }
                Publicacion respuesta = response.Lista.Find(p => p.IdPublicacion == IdPublicacion);
                if (respuesta == null)
                {
                    return new ClaseRespuesta<Publicacion>("No existe la publicacion");
                }
                Reaccion respuestaReaccion = respuesta.Reacciones.Find(r => r.Codigo == reaccion.Codigo);
                if (respuestaReaccion == null)
                {
                    return new ClaseRespuesta<Publicacion>("La reaccion no existe");
                }
                respuesta.Reacciones.Remove(respuestaReaccion);
                _context.Publicacions.Update(respuesta);
                _context.SaveChanges();
                return new ClaseRespuesta<Publicacion>(respuesta);
            }
            catch(Exception e)
            {
                return new ClaseRespuesta<Publicacion>($"Error en la aplicacion {e.Message}");
            }
        }
    }
    
}