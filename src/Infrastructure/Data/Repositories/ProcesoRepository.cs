using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProcesoRepository : RepositoryBase<Proceso>, IProcesoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProcesoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proceso>> GetAllAsync()
        {
            return await ListAsync();
        }

        public async Task<Proceso?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task AddAsync(Proceso proceso)
        {
            await base.AddAsync(proceso);
        }

        public async Task UpdateAsync(Proceso proceso)
        {
            await base.UpdateAsync(proceso);
        }
    }
}