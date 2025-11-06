using Application.Interfaces;
using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductoController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [Authorize(Roles = "Oficina")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _productoService.GetAllAsync();
        return Ok(productos);
    }

    [Authorize(Roles = "Oficina, Encargado, Operario")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        return producto == null ? NotFound() : Ok(producto);
    }

    [Authorize(Roles = "Oficina")]
    [HttpPost]
    public async Task<IActionResult> Add(ProductoCreateDTO dto)
    {
        var producto = await _productoService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [Authorize(Roles = "Oficina")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductoDTO dto)
    {
        var updated = await _productoService.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [Authorize(Roles = "Oficina")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productoService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}