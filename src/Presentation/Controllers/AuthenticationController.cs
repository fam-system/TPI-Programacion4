using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuarioService;

        public AuthenticationController(IConfiguration config, IUsuarioService usuarioService)
        {
            _config = config;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.usuario) || string.IsNullOrEmpty(credentials.password))
                return BadRequest("Debe ingresar usuario y contraseña.");

            //Buscar usuario por nombre
            Usuario? user = await _usuarioService.GetByUsernameAsync(credentials.usuario);

            if (user is null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            //Verificar contraseña usando BCrypt
            bool passwordValida = BCrypt.Net.BCrypt.Verify(credentials.password, user.PasswordHash);

            if (!passwordValida)
                return Unauthorized("Usuario o contraseña incorrectos.");

            //Generar clave simétrica para firmar el token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var credentialsFirma = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Crear claims con la info del usuario
            var claims = new List<Claim>
            {
                new Claim("usuario", user.NombreUsuario),
                new Claim("rol", user.Rol.ToString())
            };

            
            var token = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentialsFirma
            );

            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            //Retornar token y datos básicos del usuario
            return Ok(new
            {
                token = tokenString,
                usuario = user.NombreUsuario,
                rol = user.Rol.ToString()
            });
        }
    }
}