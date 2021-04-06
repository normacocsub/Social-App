using System.Linq;
using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;

namespace SocialAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly PublicacionService _service;

        public PublicacionController(SocialAppContext context)
        {
            _service = new PublicacionService(context);
        }

        [HttpPost]
        public ActionResult<PublicacionViewModel> GuardarPublicacion(PublicacionInputModel publicacionInput)
        {
            Publicacion publicacion = MapearPublicacion(publicacionInput);
            var response = _service.GuardarPublicacion(publicacion);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guarda la publicacion ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            return Ok(response.Publicacion);
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


        private Publicacion MapearPublicacion(PublicacionInputModel publicacionInput)
        {
            var publicacion = new Publicacion
            {
                Nombre = publicacionInput.Nombre,
                ContenidoPublicacion = publicacionInput.ContenidoPublicacion,
                Imagen = publicacionInput.Imagen,
                Comentarios = publicacionInput.Comentarios
            };
            return publicacion;
        }
    }
}