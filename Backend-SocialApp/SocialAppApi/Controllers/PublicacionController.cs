using System;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Hosting;
using SocialAppApi.Hubs;
using SocialAppApi.Models;

namespace SocialAppApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly PublicacionService _service;

        private readonly IHubContext<SignalHub> _hubContext;
        public PublicacionController(SocialAppContext context, IHubContext<SignalHub> hubContext, IWebHostEnvironment _environment)
        {
            _service = new PublicacionService(context,  _environment);
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<PublicacionViewModel>> GuardarPublicacion(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            publicacion.Nombre = publicacion.Usuario.Nombres + " " + publicacion.Usuario.Apellidos;
            publicacion.IdUsuario = publicacion.Usuario.Correo;
            publicacion.Fecha = DateTime.Now;
            var response = _service.GuardarPublicacion(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guarda la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpGet]
        public async Task<ActionResult<PublicacionViewModel>> ConsultarPublicacion()
        {
            var response = await _service.ConsultarPublicaciones();
            if(response.Error)
            {
                ModelState.AddModelError("Error al Consultar las publicaciones ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            return Ok(response.Lista.Select(a => new PublicacionViewModel(a)).OrderByDescending(p => p.Fecha));
        }

    


        [HttpPut]
        public async Task<ActionResult<PublicacionViewModel>> EditarPublicacion(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            publicacion.IdPublicacion = publicacionInput.IdPublicacion;
            var response = _service.EditarPublicaciones(publicacion);
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpDelete("{publicacion}")]
        public async Task<ActionResult<PublicacionViewModel>> EliminarPublicacion(string publicacion)
        {
            var response = _service.EliminarPublicacion(publicacion);
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpPut("Comentarios")]
        public async Task<ActionResult<ComentarioViewModel>> AgregarComentario(ComentarioInputModel comentarioInput)
        {
            Comentario comentario = MapearComentario(comentarioInput);
            comentario.Fecha = DateTime.Now;
            var response = _service.AgregarComentarios(comentario);
            
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }
        
        [HttpPut("EditarComentario")]
        public async Task<ActionResult<ComentarioViewModel>> EditarComentario(ComentarioInputModel comentarioInput)
        {
            Comentario comentario = MapearComentario(comentarioInput);
            var response = await _service.EditarComentario(comentario);
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpPut("Reaccion")]
        public async Task<ActionResult<ReaccionViewModel>> ActualizarReaccion(ReaccionInputModel reaccionInput)
        {
            Reaccion reaccion = MapearReaccion(reaccionInput);
            var resultado = _service.EditarReaccion(reaccion);

            if(resultado.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(resultado.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);    
        }

        [HttpDelete("Reaccion/{codigo}/{idPublicacion}")]
        public async Task<ActionResult<PublicacionViewModel>> EliminarReaccion(string codigo, string idPublicacion)
        {
            var response = await  _service.EliminarReaccion(codigo, idPublicacion);
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpDelete("Comentario/{codigo}/{publicacion}")]
        public async Task<ActionResult<PublicacionViewModel>> EliminarComentario(string codigo, string publicacion)
        {
            var response = await _service.EliminarComentario(codigo, publicacion);
            if(response.Error)
            {
                return BadRequest();
            }
            var publicacionview = new PublicacionViewModel(response.Objeto);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }
        private Reaccion MapearReaccion(ReaccionInputModel reaccionInput)
        {
            var reaccion = new Reaccion
            {
                Like = reaccionInput.Like,
                Love = reaccionInput.Love,
                IdUsuario = reaccionInput.IdUsuario,
                IdPublicacion = reaccionInput.IdPublicacion,
            };
            return reaccion;
        }

        private Comentario MapearComentario(ComentarioInputModel comentarioInput)
        {
            var comentario = new Comentario
            {
                IdComentario = comentarioInput.IdComentario,
                ContenidoComentario = comentarioInput.ContenidoComentario,
                IdPublicacion = comentarioInput.PublicacionId,
                Usuario = comentarioInput.Usuario,
                IdUsuario = comentarioInput.IdUsuario,
            };
            return comentario;
        }
        private Publicacion MapearPublicacion(PublicacionInputModel publicacionInput)
        {
            var publicacion = new Publicacion
            {
                Nombre = publicacionInput.Nombre,
                ContenidoPublicacion = publicacionInput.ContenidoPublicacion,
                Imagen = publicacionInput.Imagen,
                Comentarios = publicacionInput.Comentarios,
                Usuario = publicacionInput.Usuario,
                Reacciones = publicacionInput.Reacciones,
                
            };
            return publicacion;
        }
    }
}