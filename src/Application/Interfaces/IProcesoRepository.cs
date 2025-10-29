using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IProcesoRepository : IRepositoryBase<Proceso>
    {
        Task<IEnumerable<Proceso>> GetAllAsync();
        Task<Proceso?> GetByIdAsync(int id);
        Task AddAsync(Proceso proceso);
        Task UpdateAsync(Proceso proceso);
    }
}