using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public class ProcesoRepository : RepositoryBase<Proceso>, IProcesoRepository
    {
        public ProcesoRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
}