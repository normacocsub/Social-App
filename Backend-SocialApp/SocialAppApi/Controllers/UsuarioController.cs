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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(SocialAppContext context)
        {
            _service = new UsuarioService(context);
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
            response.Usuario.Password = "";
            response.Usuario.KeyPasswordDesEncriptar = "";
            return Ok(response.Usuario);
        }

        [HttpPost("login")]
        public ActionResult<UsuarioViewModel> Login(UsuarioInputModel usuarioInput)
        {
            Usuario usuario = MapearUsuario(usuarioInput);
            var response = _service.ValidarUsuario(usuario);

            if (response.Error)
            {
                ModelState.AddModelError("Error al iniciar Sesion", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if (response.Estado == "Invalid")
                {
                    detallesproblemas.Status = StatusCodes.Status403Forbidden;
                }

                if (response.Estado == "No Existe")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }

                if (response.Estado == "Aplication")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detallesproblemas);
            }
            response.Usuario.Password = "";
            response.Usuario.KeyPasswordDesEncriptar = "";
            return Ok(response.Usuario);
        }

        [HttpPut("Imagen")]
        public ActionResult<UsuarioViewModel> EditarImagenUsuario(UsuarioInputModel usuarioInputModel)
        {
            Usuario usuario = new Usuario();
            usuario.Correo = usuarioInputModel.Correo;
            usuario.ImagePerfil = usuarioInputModel.ImagePerfil;

            var response = _service.EditarImagenUsuario(usuario);
            if (response.Error)
            {
                ModelState.AddModelError("Error al editar el usuario", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);
                if (response.Estado == "Aplication")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if (response.Estado == "NoExiste")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }
                return BadRequest(detallesproblemas);
            }
            response.Usuario.Password = "";
            response.Usuario.KeyPasswordDesEncriptar = "";
            return Ok(response.Usuario);
        }

        [HttpGet("usuario/{correo}")]
        public ActionResult<UsuarioViewModel> BuscarUsuario(string correo)
        {
            var response = _service.BuscarUsuario(correo);
             if (response.Error)
            {
                ModelState.AddModelError("Error al editar el usuario", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);
                if (response.Estado == "Aplication")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if (response.Estado == "NoExiste")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }
                return BadRequest(detallesproblemas);
            }
            response.Usuario.Password = "";
            response.Usuario.KeyPasswordDesEncriptar = "";
            return Ok(response.Usuario);

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