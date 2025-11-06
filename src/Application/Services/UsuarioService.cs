using Application.Interfaces;
using Application.Models;
using Application.Models.CreateDTO;
using BCrypt.Net;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Estado = u.Estado.ToString(),
                Rol = u.Rol.ToString()
            });
        }

        public async Task<IEnumerable<UsuarioDTO>> GetActivosAsync()
        {
            var usuarios = await _usuarioRepository.GetActivosAsync();

            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Estado = u.Estado.ToString(),
                Rol = u.Rol.ToString()
            });
        }

        public async Task<Usuario?> GetByUsernameAsync(string username) =>
            await _usuarioRepository.GetByUsernameAsync(username);

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Estado = usuario.Estado.ToString(),
                Rol = usuario.Rol.ToString()
            };
        }

        public async Task<UsuarioDTO> CreateAsync(UsuarioCreateDTO dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Dni = dto.Dni,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                FechaIngreso = dto.FechaIngreso,
                NombreUsuario = dto.NombreUsuario,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Estado = Enum.Parse<EstadoEmpleado>(dto.Estado),
                Rol = Enum.Parse<Rol>(dto.Rol)
            };

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Estado = usuario.Estado.ToString(),
                Rol = usuario.Rol.ToString()
            };
        }

        public async Task UpdateAsync(int id, UsuarioCreateDTO dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.Dni = dto.Dni;
            usuario.Direccion = dto.Direccion;
            usuario.Telefono = dto.Telefono;
            usuario.FechaIngreso = dto.FechaIngreso;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Estado = Enum.Parse<EstadoEmpleado>(dto.Estado);
            usuario.Rol = Enum.Parse<Rol>(dto.Rol);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            usuario.Estado = EstadoEmpleado.Inactivo;
            await _usuarioRepository.UpdateAsync(usuario);
        }
    }
}