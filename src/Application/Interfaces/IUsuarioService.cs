using Application.Models;
using Application.Models.CreateDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<IEnumerable<UsuarioDTO>> GetActivosAsync();
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<UsuarioDTO> CreateAsync(UsuarioCreateDTO dto);
        Task UpdateAsync(int id, UsuarioCreateDTO dto);
        Task DeleteAsync(int id);
    }
}