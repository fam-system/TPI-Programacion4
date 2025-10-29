using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await ListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task AddAsync(Producto producto)
        {
            await base.AddAsync(producto);
        }

        public async Task UpdateAsync(Producto producto)
        {
            await base.UpdateAsync(producto);
        }
    }
}