using Application.Interfaces;
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

        public async Task<IEnumerable<Proceso>> GetAllAsync()
        {
            return await _procesoRepository.GetAllAsync();
        }

        public async Task<Proceso?> GetByIdAsync(int id)
        {
            return await _procesoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Proceso proceso)
        {
            await _procesoRepository.AddAsync(proceso);
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
            {
                await _procesoRepository.DeleteAsync(proceso);
            }
        }
    }
}