using Application.Interfaces;
using Application.Models;
using Application.Models.CreateDTO;
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
            var created = await _procesoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Oficina, Encargado")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcesoDTO>>> GetAll()
        {
            var procesos = await _procesoService.GetAllAsync();
            return Ok(procesos);
        }

        [Authorize(Roles = "Oficina, Encargado, Operario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcesoDTO>> GetById(int id)
        {
            var proceso = await _procesoService.GetByIdAsync(id);
            if (proceso == null) return NotFound();
            return Ok(proceso);
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
