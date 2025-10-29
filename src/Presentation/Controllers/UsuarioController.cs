using Application.Interfaces;
using Application.Models;
using BCrypt.Net;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET api/usuarios
        [Authorize(Roles = "Oficina")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var dtoList = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Estado = u.Estado.ToString(),
                Rol = u.Rol.ToString()
            });
            return Ok(dtoList);
        }


        //GET api/usuarios/activos
        [Authorize(Roles = "Oficina")]
        [HttpGet("Activos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetActivos()
        {
            var usuarios = await _usuarioRepository.GetActivosAsync();
            var dtoList = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Estado = u.Estado.ToString(),
                Rol = u.Rol.ToString()
            });
            return Ok(dtoList);
        }

        // GET api/usuarios/{id}
        [Authorize(Roles = "Oficina, Encargado")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            var dto = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Estado = usuario.Estado.ToString(),
                Rol = usuario.Rol.ToString()
            };

            return Ok(dto);
        }

        // POST api/usuarios
        [Authorize(Roles = "Oficina")]
        [HttpPost]
        public async Task<ActionResult> Create(UsuarioCreateDTO dto)
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
                Estado = Enum.Parse<Domain.Enums.EstadoEmpleado>(dto.Estado),
                Rol = Enum.Parse<Domain.Enums.Rol>(dto.Rol)
            };

            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, null);
        }

        // PUT api/usuarios/{id}
        [Authorize(Roles = "Oficina")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioCreateDTO dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.Dni = dto.Dni;
            usuario.Direccion = dto.Direccion;
            usuario.Telefono = dto.Telefono;
            usuario.FechaIngreso = dto.FechaIngreso;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Estado = Enum.Parse<Domain.Enums.EstadoEmpleado>(dto.Estado);
            usuario.Rol = Enum.Parse<Domain.Enums.Rol>(dto.Rol);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }
        //baja logica de empleado
        [Authorize(Roles = "Oficina")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            usuario.Estado = EstadoEmpleado.Inactivo;
            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

    }
}