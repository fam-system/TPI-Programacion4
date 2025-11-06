using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<IEnumerable<Usuario>> GetActivosAsync();
        Task<Usuario?> GetByUsernameAsync(string username);
    }
}