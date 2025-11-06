using Application.Interfaces;
using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _productoRepository.GetAllAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task<Producto> AddAsync(ProductoCreateDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre
            };

            await _productoRepository.AddAsync(producto);
            return producto;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductoDTO dto)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return false;

            producto.Nombre = dto.Nombre;
            await _productoRepository.UpdateAsync(producto);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return false;

            await _productoRepository.DeleteAsync(producto);
            return true;
        }
    }
}