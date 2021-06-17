using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Logica;
using SocialAppApi.Config;
using Microsoft.Extensions.Options;
using SocialAppApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
namespace SocialAppApi.Servicios
{
    public class ServiciosJwt
    {
        private readonly AppSetting _appSetting;
        public ServiciosJwt(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public UsuarioViewModel GenerarToken(Usuario usuario)
        {
            if(usuario == null)
                return null;
            
            var usuarioResponse = new UsuarioViewModel(usuario)
            {
                Correo = usuario.Correo,
            };
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var claims = new List<Claim>();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, usuario.Correo.ToString())
                }),
                
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuarioResponse.Token = tokenHandler.WriteToken(token);

            return usuarioResponse;
        }
    }
}