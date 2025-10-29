using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IProductoRepository : IRepositoryBase<Producto>
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task AddAsync(Producto producto);
        Task UpdateAsync(Producto producto);
    }
}