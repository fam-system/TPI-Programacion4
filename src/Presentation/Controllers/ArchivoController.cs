using Application.Interfaces;
using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;

        public ArchivoController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [Authorize(Roles = "Oficina")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archivo>>> GetAll()
        {
            var archivos = await _archivoService.GetAllAsync();
            return Ok(archivos);
        }

        [Authorize(Roles = "Oficina, Encargado, Operario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Archivo>> GetById(int id)
        {
            var archivo = await _archivoService.GetByIdAsync(id);
            if (archivo == null) return NotFound();
            return Ok(archivo);
        }

        [Authorize(Roles = "Oficina")]
        [HttpPost]
        public async Task<IActionResult> Add(ArchivoCreateDTO dto)
        {
            await _archivoService.AddAsync(dto);
            return Ok(new { message = "Archivo creado correctamente" });
        }

        [Authorize(Roles = "Oficina")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateArchivoDTO dto)
        {
            try
            {
                await _archivoService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Oficina")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _archivoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}