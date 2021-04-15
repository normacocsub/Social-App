using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Logica
{
    public class PublicacionService
    {
        private SocialAppContext _context;

        public PublicacionService(SocialAppContext context)
        {
            _context = context;
        }

        public GuardarPublicacionResponse GuardarPublicacion(Publicacion publicacion)
        {
            try
            {
                publicacion.IdPublicacion = Seguridad.RandomString(16);
                _context.Publicacions.Add(publicacion);
                _context.SaveChanges();
                return new GuardarPublicacionResponse(publicacion);
            }
            catch(Exception e)
            {
                return new GuardarPublicacionResponse($"Error aplicacion: {e.Message}");
            }
        }

        public ConsultarPublicacionesResponse ConsultarPublicaciones()
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(p => p.Comentarios.OrderByDescending(c => c.Fecha))
                .Include(p => p.Reacciones)
                .ToList().OrderByDescending(p => p.Fecha);
                foreach (var item in publicaciones)
                {
                    item.Usuario = _context.Usuarios.Find(item.IdUsuario);
                    foreach (var item2 in item.Comentarios)
                    {
                        item2.Usuario = _context.Usuarios.Find(item2.IdUsuario);
                    }
                    foreach (var item3 in item.Reacciones)
                    {
                        item3.Usuario = _context.Usuarios.Find(item3.IdUsuario);
                    }
                }
                return new ConsultarPublicacionesResponse(_context.Publicacions.Include(p => p.Comentarios
                .OrderByDescending(c => c.Fecha)).ToList());
            }
            catch(Exception e)
            {
                return new ConsultarPublicacionesResponse($"Error Aplication: {e.Message}");
            }
        }

        public EditarPublicacionesReponse EditarPublicaciones(Publicacion publicacion)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(c => c.Comentarios).ToList();
                var response = publicaciones.Find( p => p.IdPublicacion == publicacion.IdPublicacion);
                if(response != null)
                {
                    response.ContenidoPublicacion = publicacion.ContenidoPublicacion;
                    _context.Publicacions.Update(response);
                    _context.SaveChanges();
                    return new EditarPublicacionesReponse(response);
                }
                else
                {
                    return new EditarPublicacionesReponse("No existe la publicacion ", "No existe");
                }
            }
            catch(Exception e)
            {
                return new EditarPublicacionesReponse($"Error en la aplicacion: {e.Message}", "Aplication");
            }
        }




        public EditarPublicacionesReponse AgregarComentarios(Comentario comentario)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(c => c.Comentarios).ToList();
                var response = publicaciones.Find( p => p.IdPublicacion == comentario.PublicacionId);
                if(response != null)
                {
                    response.Comentarios.Add(comentario);
                   
                    foreach (var item in response.Comentarios)
                    {

                        if(item.IdComentario == "")
                        {
                            item.IdComentario = Seguridad.RandomString(16);
                        }
                    }
                    _context.Publicacions.Update(response);
                    _context.SaveChanges();
                    return new EditarPublicacionesReponse(response);
                }
                else
                {
                    return new EditarPublicacionesReponse("No existe la publicacion ", "No existe");
                }
            }
            catch(Exception e)
            {
                return new EditarPublicacionesReponse($"Error en la aplicacion: {e.Message}", "Aplication");
            }
        }


        public EliminarPublicacionesResponse EliminarPublicacion(string publicacion)
        {
            try
            {
                var response = _context.Publicacions.Find(publicacion);
                if(response != null)
                {
                    _context.Publicacions.Remove(response);
                    _context.SaveChanges();
                    return new EliminarPublicacionesResponse(response);
                }
                else
                {
                    return new EliminarPublicacionesResponse("Error: No existe la publicacion", "No existe");
                }
            }
            catch(Exception e)
            {
                return new EliminarPublicacionesResponse($"Error en la aplicacion {e.Message}", "Aplication");
            }
        }

        public EditarComentarioResponse EditarComentario(Comentario comentario)
        {
            try
            {
                var comentarios = _context.Comentarios.ToList();
                var response = comentarios.Find(c => c.IdComentario == comentario.IdComentario);
                if(response != null)
                {
                    response.Usuario = _context.Usuarios.Find(response.IdUsuario);
                    response.ContenidoComentario = comentario.ContenidoComentario;

                    if(response.Usuario.Correo != comentario.Usuario.Correo)
                    {
                        return new EditarComentarioResponse("El usuario no puede editar este comentario", "!Editar");
                    }
                    else
                    {
                        _context.Comentarios.Update(response);
                        _context.SaveChanges();
                        

                        var publicaciones = _context.Publicacions.Include(p => p.Comentarios).ToList();
                        foreach (var item in publicaciones)
                        {
                            item.Usuario = _context.Usuarios.Find(item.IdUsuario);
                            foreach (var item2 in item.Comentarios)
                            {
                                item2.Usuario = _context.Usuarios.Find(item2.IdUsuario);
                            }
                        }

                        var responsePublicacion = publicaciones.Find(c => c.IdPublicacion == comentario.PublicacionId);
                        return new EditarComentarioResponse(responsePublicacion);
                    }
                }
                else
                {
                    return new EditarComentarioResponse("No se encuentra el comentario", "No Existe");
                }
            }
            catch(Exception e)
            {
                return new EditarComentarioResponse($"Error en la aplicacion {e.Message}", "Aplication");
            }
        }

        public EditarReaccionResponse EditarReaccion(Reaccion reaccion)
        {
            try
            {
                var publicaciones = _context.Publicacions.Include(p => p.Comentarios)
                .Include(p => p.Reacciones)
                .ToList();
                var response = publicaciones.Find(p => p.IdPublicacion == reaccion.IdPublicacion);
                if(response != null)
                {
                    if(reaccion.Like && reaccion.Love)
                    {
                        return new EditarReaccionResponse("No se puede usar las dos reacciones a la vez", "TwoReacciones");
                    }
                    if(reaccion.Like == false && reaccion.Love == false)
                    {
                        return new EditarReaccionResponse("No se encuentra ninguna reaccion", "TwoReacciones");
                    }

                    response.agregarReaccion(reaccion);
                    _context.Publicacions.Update(response);
                    _context.SaveChanges();
                    return new EditarReaccionResponse(response);
                }
                else
                {
                    return new EditarReaccionResponse("No se encontro la publicacion", "NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EditarReaccionResponse($"Error en la aplicacion {e.Message}", "Aplication");
            }
        }


        public class EditarReaccionResponse
        {
            public EditarReaccionResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public EditarReaccionResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public Publicacion Publicacion { get; set; }
        }

        public class EditarComentarioResponse
        {
            public EditarComentarioResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public EditarComentarioResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }

            public bool Error { get; set; }
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public Publicacion Publicacion { get; set; }
        }
        public class EliminarPublicacionesResponse
        {
            public EliminarPublicacionesResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public EliminarPublicacionesResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public Publicacion Publicacion { get; set; }
        }


        public class EditarPublicacionesReponse
        {
            public EditarPublicacionesReponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;

            }

            public EditarPublicacionesReponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public string Estado { get; set; }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Publicacion Publicacion { get; set; }
        }

        public class ConsultarPublicacionesResponse
        {
            public ConsultarPublicacionesResponse(List<Publicacion> publicacions)
            {
                Error = false;
                Publicaciones = publicacions;
            }

            public ConsultarPublicacionesResponse(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public List<Publicacion> Publicaciones { get; set; }
        }

        public class GuardarPublicacionResponse
        {
            public GuardarPublicacionResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public GuardarPublicacionResponse(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Publicacion Publicacion { get; set; }
        }
    }
}