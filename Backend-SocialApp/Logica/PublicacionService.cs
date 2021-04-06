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
                return new ConsultarPublicacionesResponse(_context.Publicacions.Include(p => p.Comentarios).ToList());
            }
            catch(Exception e)
            {
                return new ConsultarPublicacionesResponse($"Error Aplication: {e.Message}");
            }
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