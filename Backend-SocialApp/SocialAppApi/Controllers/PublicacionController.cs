using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using SocialAppApi.Hubs;
using SocialAppApi.Models;

namespace SocialAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly PublicacionService _service;

        private readonly IHubContext<SignalHub> _hubContext;
        public PublicacionController(SocialAppContext context, IHubContext<SignalHub> hubContext)
        {
            _service = new PublicacionService(context);
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<PublicacionViewModel>> GuardarPublicacion(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            publicacion.Nombre = publicacion.Usuario.Nombres + " " + publicacion.Usuario.Apellidos;
            publicacion.IdUsuario = publicacion.Usuario.Correo;
            var response = _service.GuardarPublicacion(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guarda la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Publicacion);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpGet]
        public ActionResult<PublicacionViewModel> ConsultarPublicacion()
        {
            var response = _service.ConsultarPublicaciones();
            if(response.Error)
            {
                ModelState.AddModelError("Error al Consultar las publicaciones ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            return Ok(response.Publicaciones.Select(a => new PublicacionViewModel(a)));
        }


        [HttpPut]
        public async Task<ActionResult<PublicacionViewModel>> EditarPublicacion(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            publicacion.IdPublicacion = publicacionInput.IdPublicacion;
            var response = _service.EditarPublicaciones(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al Actualizar la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                if(response.Estado == "No existe")
                {
                    detalleProblemas.Status = StatusCodes.Status404NotFound;
                }
                if(response.Estado == "Aplication")
                {
                    detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Publicacion);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpDelete("{publicacion}")]
        public async Task<ActionResult<PublicacionViewModel>> EliminarPublicacion(string publicacion)
        {
            var response = _service.EliminarPublicacion(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al Eliminar la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                if(response.Estado == "No existe")
                {
                    detalleProblemas.Status = StatusCodes.Status404NotFound;
                }
                if(response.Estado == "Aplication")
                {
                    detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Publicacion);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }

        [HttpPut("Comentarios")]
        public async Task<ActionResult<PublicacionViewModel>> AgregarComentario(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            publicacion.IdPublicacion = publicacionInput.IdPublicacion;
            publicacion.AgregarIdComentarios();
            
            var response = _service.AgregarComentarios(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al Actualizar la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                if(response.Estado == "No existe")
                {
                    detalleProblemas.Status = StatusCodes.Status404NotFound;
                }
                if(response.Estado == "Aplication")
                {
                    detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Publicacion);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }


        [HttpPut("EditarComentario")]
        public async Task<ActionResult<ComentarioViewModel>> EditarComentario(ComentarioInputModel comentarioInput)
        {
            Comentario comentario = MapearComentario(comentarioInput);
            var response = _service.EditarComentario(comentario);
            if(response.Error)
            {
                ModelState.AddModelError("Error al Editar el comentario ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                if(response.Estado == "!Editar")
                {
                    detalleProblemas.Status = StatusCodes.Status401Unauthorized;
                }
                if(response.Estado == "No Existe")
                {
                    detalleProblemas.Status = StatusCodes.Status404NotFound;
                }
                if(response.Estado == "Aplication")
                {
                    detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detalleProblemas);
            }
            var publicacionview = new PublicacionViewModel(response.Publicacion);
            await _hubContext.Clients.All.SendAsync("publicacion", publicacionview);
            return Ok(publicacionview);
        }


        private Comentario MapearComentario(ComentarioInputModel comentarioInput)
        {
            var comentario = new Comentario
            {
                IdComentario = comentarioInput.IdComentario,
                ContenidoComentario = comentarioInput.ContenidoComentario,
                PublicacionId = comentarioInput.PublicacionId,
                Usuario = comentarioInput.Usuario,
                IdUsuario = comentarioInput.IdUsuario
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
            };
            return publicacion;
        }
    }
}