using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IArchivoRepository : IRepositoryBase<Archivo>
    {
        Task<IEnumerable<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
    }
}