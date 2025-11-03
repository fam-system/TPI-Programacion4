using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IProductoRepository _repository;

    public ProductoController(IProductoRepository repository)
    {
        _repository = repository;
    }
    [Authorize(Roles = "Oficina")]
    [HttpGet]
    public async Task<IEnumerable<Producto>> GetAll()
    {
        return await _repository.GetAllAsync();
    }
    [Authorize(Roles = "Oficina, Encargado, Operario")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetById(int id)
    {
        var producto = await _repository.GetByIdAsync(id);
        if (producto == null) return NotFound();
        return producto;
    }

    [Authorize(Roles = "Oficina")]
    [HttpPost]
    public async Task<IActionResult> Add(ProductoCreateDTO dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre
        };
        await _repository.AddAsync(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }
    [Authorize(Roles = "Oficina")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductoDTO dto)
    {
        var producto = await _repository.GetByIdAsync(id);
        if (producto == null)
            return NotFound();

        producto.Nombre = dto.Nombre;
  
        await _repository.UpdateAsync(producto);

        return NoContent();
    }

    [Authorize(Roles = "Oficina")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _repository.GetByIdAsync(id);
        if (producto == null) return NotFound();
        await _repository.DeleteAsync(producto);
        return NoContent();
    }
}