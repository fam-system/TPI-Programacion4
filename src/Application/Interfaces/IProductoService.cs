using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<Producto> AddAsync(ProductoCreateDTO dto);
        Task<bool> UpdateAsync(int id, UpdateProductoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}