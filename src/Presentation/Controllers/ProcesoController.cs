using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcesoController : ControllerBase
    {
        private readonly IProcesoRepository _procesoRepository;

        public ProcesoController(IProcesoRepository procesoRepository)
        {
            _procesoRepository = procesoRepository;
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

            await _procesoRepository.AddAsync(proceso);
            return CreatedAtAction(nameof(GetById), new { id = proceso.Id }, proceso);
        }

       
        [Authorize(Roles = "Oficina, Encargado")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcesoDTO>>> GetAll()
        {
            var procesos = await _procesoRepository.GetAllAsync();
            var result = new List<ProcesoDTO>();

            foreach (var p in procesos)
            {
                result.Add(new ProcesoDTO
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
            }

            return Ok(result);
        }

      
        [Authorize(Roles = "Oficina, Encargado, Operario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcesoDTO>> GetById(int id)
        {
            var p = await _procesoRepository.GetByIdAsync(id);
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
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso == null) return NotFound();

            proceso.FechaInicio = DateTime.Now;
            proceso.EstadoProceso = "En Proceso";
            await _procesoRepository.UpdateAsync(proceso);

            return NoContent();
        }

        // PUT: api/Proceso/End/{id}
        [Authorize(Roles = "Operario")]
        [HttpPut("End/{id}")]
        public async Task<ActionResult> EndProceso(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso == null) return NotFound();

            proceso.FechaFin = DateTime.Now;
            proceso.EstadoProceso = "Finalizado";
            await _procesoRepository.UpdateAsync(proceso);

            return NoContent();
        }

        [Authorize(Roles = "Oficina")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso == null) return NotFound();
            await _procesoRepository.DeleteAsync(proceso);
            return NoContent();
        }
    }
}