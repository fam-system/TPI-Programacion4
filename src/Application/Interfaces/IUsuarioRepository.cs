using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<IEnumerable<Usuario>> GetActivosAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task<Usuario?> GetByUsernameAsync(string username);
    }
}