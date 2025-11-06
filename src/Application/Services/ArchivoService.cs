using Application.Interfaces;
using Application.Models.CreateDTO;
using Application.Models.UpdateDTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;

        public ArchivoService(IArchivoRepository archivoRepository)
        {
            _archivoRepository = archivoRepository;
        }

        
        public async Task<IEnumerable<Archivo>> GetAllAsync()
        {
            return await _archivoRepository.GetAllAsync();
        }

        
        public async Task<Archivo?> GetByIdAsync(int id)
        {
            return await _archivoRepository.GetByIdAsync(id);
        }


        public async Task<Archivo> AddAsync(ArchivoCreateDTO dto)
        {
            var nuevoArchivo = new Archivo
            {
                Nombre = dto.Nombre,
                ProductoId = dto.ProductoId
            };

            await _archivoRepository.AddAsync(nuevoArchivo);
            return nuevoArchivo;
        }


        public async Task UpdateAsync(int id, UpdateArchivoDTO dto)
        {
            var archivoExistente = await _archivoRepository.GetByIdAsync(id);
            if (archivoExistente == null)
                throw new KeyNotFoundException("No se encontró el archivo");

            archivoExistente.Nombre = dto.Nombre;

            await _archivoRepository.UpdateAsync(archivoExistente);
        }

       
        public async Task DeleteAsync(int id)
        {
            var archivo = await _archivoRepository.GetByIdAsync(id);
            if (archivo == null)
                throw new KeyNotFoundException("No se encontró el archivo");

            await _archivoRepository.DeleteAsync(archivo);
        }
    }
}