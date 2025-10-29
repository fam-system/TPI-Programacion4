using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> AuthenticateAsync(string username, string password);
        Task<Usuario?> GetByUsernameAsync(string usuario);
    }
}