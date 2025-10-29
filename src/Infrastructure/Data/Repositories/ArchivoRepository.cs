using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ArchivoRepository : RepositoryBase<Archivo>, IArchivoRepository
    {
        private readonly ApplicationDbContext _context;

        public ArchivoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Archivo>> GetAllAsync()
        {
            return await ListAsync();
        }

        public async Task<Archivo?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task AddAsync(Archivo archivo)
        {
            await base.AddAsync(archivo);
        }

        public async Task UpdateAsync(Archivo archivo)
        {
            await base.UpdateAsync(archivo);
        }

        public async Task DeleteAsync(Archivo archivo)
        {
            await base.DeleteAsync(archivo);
        }
    }
}