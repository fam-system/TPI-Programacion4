using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public class ArchivoRepository : RepositoryBase<Archivo>, IArchivoRepository
    {
        public ArchivoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}