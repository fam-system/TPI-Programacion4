using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProcesoService
    {
        Task<IEnumerable<Proceso>> GetAllAsync();
        Task<Proceso?> GetByIdAsync(int id);
        Task AddAsync(Proceso proceso);
        Task StartProcesoAsync(int id);
        Task EndProcesoAsync(int id);
        Task DeleteAsync(int id);
    }
}