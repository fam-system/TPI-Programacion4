using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await ListAsync();
        }
        public async Task<IEnumerable<Usuario>> GetActivosAsync()
        {
            return await _context.Usuarios
                .Where(u => u.Estado == Domain.Enums.EstadoEmpleado.Activo)
                .ToListAsync();
        }
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            
            return await base.GetByIdAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await base.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await base.UpdateAsync(usuario);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == username);
        }

    }
}