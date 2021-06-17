using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialAppApi.Config;
using SocialAppApi.Models;
using SocialAppApi.Servicios;

namespace SocialAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        private ServiciosJwt _servicioJwt;
        public UsuarioController(SocialAppContext context, IOptions<AppSetting> appSettings, IWebHostEnvironment _environment)
        {
            _servicioJwt = new ServiciosJwt(appSettings);
            _service = new UsuarioService(context, _environment);
        }

        [HttpPost]
        public ActionResult<UsuarioViewModel> Post(UsuarioInputModel usuarioInput)
        {
            Usuario usuario = MapearUsuario(usuarioInput);
            var response = _service.GuardarUsuario(usuario);

            if (response.Error)
            {
                ModelState.AddModelError("Error al guarda el usuario ", response.Mensaje);
                var detalleProblemas = new ValidationProblemDetails(ModelState);
                detalleProblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detalleProblemas);
            }
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(response.Objeto);
            return Ok(usuarioViewModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UsuarioViewModel> Login(UsuarioInputModel usuarioInput)
        {
            Usuario usuario = MapearUsuario(usuarioInput);
            var user = _service.ValidarUsuario(usuario);

            if (user.Objeto == null)
            {
                ModelState.AddModelError("Acceso Denegado", "Usuario y/o contraseña incorrectos");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized,
                };
                return Unauthorized(problemDetails);
            }
            var response = _servicioJwt.GenerarToken(user.Objeto);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("Imagen")]
        public ActionResult<UsuarioViewModel> EditarImagenUsuario(UsuarioInputModel usuarioInputModel)
        {
            Usuario usuario = new Usuario();
            usuario.Correo = usuarioInputModel.Correo;
            usuario.ImagePerfil = usuarioInputModel.ImagePerfil;

            var response = _service.EditarImagenUsuario(usuario);
            if (response.Error)
            {
                return BadRequest();
            }
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(response.Objeto);
            return Ok(usuarioViewModel);
        }

        [Authorize]
        [HttpGet("usuario/{correo}")]
        public ActionResult<UsuarioViewModel> BuscarUsuario(string correo)
        {
            var response = _service.BuscarUsuario(correo);
             if (response.Error)
            {
                return BadRequest();
            }
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(response.Objeto);
            return Ok(usuarioViewModel);
        }

        private Usuario MapearUsuario(UsuarioInputModel usuarioInput)
        {
            var key = Seguridad.RandomString(16);
            var usuario = new Usuario
            {
                Correo = usuarioInput.Correo,
                Password = Seguridad.Encriptar(usuarioInput.Password, key),
                Nombres = usuarioInput.Nombres,
                Apellidos = usuarioInput.Apellidos,
                Sexo = usuarioInput.Sexo,
                KeyPasswordDesEncriptar = key,
                ImagePerfil = usuarioInput.ImagePerfil
            };
            return usuario;
        }
    }
}