using Application.Interfaces;
using Application.Models;
using Application.Models.CreateDTO;
using Domain.Entities;

namespace Application.Services
{
    public class ProcesoService : IProcesoService
    {
        private readonly IProcesoRepository _procesoRepository;

        public ProcesoService(IProcesoRepository procesoRepository)
        {
            _procesoRepository = procesoRepository;
        }


        public async Task<ProcesoDTO> CreateAsync(ProcesoCreateDTO dto)
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

            return MapToDTO(proceso);
        }


        public async Task<IEnumerable<ProcesoDTO>> GetAllAsync()
        {
            var procesos = await _procesoRepository.GetAllAsync();
            return procesos.Select(MapToDTO);
        }


        public async Task<ProcesoDTO?> GetByIdAsync(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            return proceso == null ? null : MapToDTO(proceso);
        }


        public async Task StartProcesoAsync(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso == null)
                throw new KeyNotFoundException("Proceso no encontrado.");

            proceso.FechaInicio = DateTime.Now;
            proceso.EstadoProceso = "En Proceso";

            await _procesoRepository.UpdateAsync(proceso);
        }

        public async Task EndProcesoAsync(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso == null)
                throw new KeyNotFoundException("Proceso no encontrado.");

            proceso.FechaFin = DateTime.Now;
            proceso.EstadoProceso = "Finalizado";

            await _procesoRepository.UpdateAsync(proceso);
        }

        public async Task DeleteAsync(int id)
        {
            var proceso = await _procesoRepository.GetByIdAsync(id);
            if (proceso != null)
                await _procesoRepository.DeleteAsync(proceso);
        }


        private static ProcesoDTO MapToDTO(Proceso p)
        {
            return new ProcesoDTO
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
        }
    }
}