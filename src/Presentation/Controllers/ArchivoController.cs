using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Models.UpdateDTO;

[ApiController]
[Route("api/[controller]")]
public class ArchivoController : ControllerBase
{
    private readonly IArchivoRepository _repository;

    public ArchivoController(IArchivoRepository repository)
    {
        _repository = repository;
    }
    [Authorize(Roles = "Oficina")]
    [HttpGet]
    public async Task<IEnumerable<Archivo>> GetAll()
    {
        return await _repository.GetAllAsync();
    }
    [Authorize(Roles = "Oficina, Encargado, Operario")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Archivo>> GetById(int id)
    {
        var archivo = await _repository.GetByIdAsync(id);
        if (archivo == null) return NotFound();
        return archivo;
    }
    [Authorize(Roles= "Oficina")]
    [HttpPost]
    public async Task<IActionResult> Add(ArchivoCreateDTO dto)
    {
        var archivo = new Archivo
        {
            Nombre = dto.Nombre,
            ProductoId = dto.ProductoId
        };
        await _repository.AddAsync(archivo);
        return CreatedAtAction(nameof(GetById), new { id = archivo.Id }, archivo);
    }


    [Authorize(Roles = "Oficina")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateArchivoDTO dto)
    {

        var archivo = await _repository.GetByIdAsync(id);
        if (archivo == null)
            return NotFound();

        archivo.Nombre = dto.Nombre;

        await _repository.UpdateAsync(archivo);

        return NoContent();
    }


    [Authorize(Roles = "Oficina")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var archivo = await _repository.GetByIdAsync(id);
        if (archivo == null) return NotFound();
        await _repository.DeleteAsync(archivo);
        return NoContent();
    }
}