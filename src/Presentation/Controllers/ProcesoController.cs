using Application.Interfaces;
using Application.Models;
using Application.Models.CreateDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcesoController : ControllerBase
    {
        private readonly IProcesoService _procesoService;

        public ProcesoController(IProcesoService procesoService)
        {
            _procesoService = procesoService;
        }

        [Authorize(Roles = "Oficina")]
        [HttpPost]
        public async Task<ActionResult> Create(ProcesoCreateDTO dto)
        {
            var proceso = new Proceso
            {
                Nombre = dto.Nombre,
                CantidadProducto = dto.CantidadProducto,
                FechaEntrega = dto.FechaEntrega ?? DateTime.Now,
                ProductoId = dto.ProductoId,
                FechaInicio = null,
                FechaFin = null,
                EstadoProceso = "Pendiente"
            };

            await _procesoService.AddAsync(proceso);
            return CreatedAtAction(nameof(GetById), new { id = proceso.Id }, proceso);
        }

        [Authorize(Roles = "Oficina, Encargado")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcesoDTO>>> GetAll()
        {
            var procesos = await _procesoService.GetAllAsync();

            var result = procesos.Select(p => new ProcesoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                CantidadProducto = p.CantidadProducto,
                EstadoProceso = p.EstadoProceso,
                FechaEntrega = p.FechaEntrega,
                ProductoId = p.ProductoId
            });

            return Ok(result);
        }

        [Authorize(Roles = "Oficina, Encargado, Operario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcesoDTO>> GetById(int id)
        {
            var p = await _procesoService.GetByIdAsync(id);
            if (p == null) return NotFound();

            var dto = new ProcesoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                CantidadProducto = p.CantidadProducto,
                EstadoProceso = p.EstadoProceso,
                FechaEntrega = p.FechaEntrega,
                ProductoId = p.ProductoId
            };

            return Ok(dto);
        }

        [Authorize(Roles = "Operario")]
        [HttpPut("Start/{id}")]
        public async Task<ActionResult> StartProceso(int id)
        {
            try
            {
                await _procesoService.StartProcesoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Operario")]
        [HttpPut("End/{id}")]
        public async Task<ActionResult> EndProceso(int id)
        {
            try
            {
                await _procesoService.EndProcesoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Oficina")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _procesoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
