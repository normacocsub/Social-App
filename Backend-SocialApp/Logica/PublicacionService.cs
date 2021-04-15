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
                var publicaciones = _context.Publicacions.Include(c => c.Comentarios)
                .ToList();
                var response = publicaciones.Find( p => p.IdPublicacion == comentario.IdPublicacion);
                if(response != null)
                {
                    response.Comentarios.Add(comentario);
                   
                    foreach (var item in response.Comentarios)
                    {

                        if(item.IdComentario == "")
                        {
                            item.IdComentario = Seguridad.RandomString(16);
                            
                        }
                        item.Usuario = _context.Usuarios.Find(item.IdUsuario);
                    }
                    response.Usuario = _context.Usuarios.Find(response.IdUsuario);
                    _context.Publicacions.Update(response);
                    _context.SaveChanges();
                    response.Comentarios = response.Comentarios.OrderByDescending(c => c.Fecha).ToList();
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
                        

                        var publicaciones = ConsultarPublicaciones();

                        if(publicaciones.Error)
                        {
                            return new EditarComentarioResponse($"Error en la aplicacion {publicaciones.Mensaje}", "Aplication");
                        }

                        var responsePublicacion = publicaciones.Publicaciones.Find(c => c.IdPublicacion == comentario.IdPublicacion);
                        responsePublicacion.Comentarios = responsePublicacion.Comentarios.OrderByDescending(c => c.Fecha).ToList();
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

        public EliminarComentarioResponse EliminarComentario(string codigo, string publicacion)
        {
            try
            {
                var publicaciones = ConsultarPublicaciones();
                if(publicaciones.Error == false)
                {
                    var respuesta = publicaciones.Publicaciones.Find(p => p.IdPublicacion == publicacion);
                    if(respuesta != null)
                    {
                        var respuestaComentario = respuesta.Comentarios.Find(c => c.IdComentario == codigo);
                        if(respuestaComentario != null)
                        {
                            respuesta.Comentarios.Remove(respuestaComentario);
                            _context.Publicacions.Update(respuesta);
                            _context.SaveChanges();
                            return new EliminarComentarioResponse(respuesta);
                        }
                        else
                        {
                            return new EliminarComentarioResponse("No existe el comentario", "NoExiste");
                        }
                    }
                    else
                    {
                        return new EliminarComentarioResponse("No existe la publicacion", "NoExiste");
                    }
                }
                else
                {
                    return new EliminarComentarioResponse("Error al consultar las publicaciones", "Aplication");
                }
            }
            catch(Exception e)
            {
                return new EliminarComentarioResponse($"Error en la aplicacion {e.Message}", "Aplication");
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

        public EliminarReaccionResponse EliminarReaccion(string codigo, string IdPublicacion)
        {
            try
            {
                var response = ConsultarPublicaciones();
                var reaccion = _context.Reacciones.Find(codigo);
                if(reaccion != null)
                {
                    if(response.Error == false)
                    {
                        var respuesta = response.Publicaciones.Find(p => p.IdPublicacion == IdPublicacion);
                        if(respuesta != null)
                        {
                            var respuestaReaccion = respuesta.Reacciones.Find(r => r.Codigo == reaccion.Codigo);
                            if(respuestaReaccion != null)
                            {
                                respuesta.Reacciones.Remove(respuestaReaccion);
                                _context.Publicacions.Update(respuesta);
                                _context.SaveChanges();
                                return new EliminarReaccionResponse(respuesta);
                            }
                            else
                            {
                                return new EliminarReaccionResponse("La reaccion no existe", "NoExiste");
                            }
                        }
                        else
                        {
                            return new EliminarReaccionResponse("No existe la publicacion", "NoExiste");
                        }
                    }
                    else
                    {
                        return new EliminarReaccionResponse($"Error en la aplicacion {response.Mensaje}", "Aplication");
                    }
                }
                else
                {
                    return new EliminarReaccionResponse("No existe la reaccion", "NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EliminarReaccionResponse($"Error en la aplicacion {e.Message}", "Aplication");
            }
        }

        public class EliminarComentarioResponse
        {
            public EliminarComentarioResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public EliminarComentarioResponse(string mensaje, string estado)
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

        public class EliminarReaccionResponse
        {
            public EliminarReaccionResponse(Publicacion publicacion)
            {
                Error = false;
                Publicacion = publicacion;
            }

            public EliminarReaccionResponse(string mensaje, string estado)
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