using Application.Interfaces;
using Application.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioRepository _usuarioRepository;

        public CustomAuthenticationService(IConfiguration config, IUsuarioRepository usuarioRepository)
        {
            _config = config;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> AutenticarAsync(AuthenticationRequest request)
        {
            var user = await _usuarioRepository.GetByUsernameAsync(request.usuario);
            if (user == null)
                return string.Empty;

            var passwordHash = ComputeSha256Hash(request.password);
            if (user.PasswordHash != passwordHash)
                return string.Empty;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("usuario", user.NombreUsuario),
                new Claim("rol", user.Rol.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

    }
}