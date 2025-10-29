using Application.Interfaces;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> AuthenticateAsync(string usuario, string password)
        {
            var user = await _usuarioRepository.GetByUsernameAsync(usuario);
            if (user == null)
                return null;

            // Aquí es donde debes hashear la contraseña recibida
            string hashedPassword = ComputeSha256Hash(password);

            if (user.PasswordHash != hashedPassword)
                return null;

            return user;
        }

        // Puedes reutilizar el mismo método de CustomAuthenticationService
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

        public async Task<Usuario?> GetByUsernameAsync(string usuario)
        {
            return await _usuarioRepository.GetByUsernameAsync(usuario);
        }
    }
}