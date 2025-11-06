using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IArchivoService
    {
        Task<IEnumerable<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task<Archivo> AddAsync(ArchivoCreateDTO dto);
        Task UpdateAsync(int id, UpdateArchivoDTO dto);
        Task DeleteAsync(int id);
    }
}