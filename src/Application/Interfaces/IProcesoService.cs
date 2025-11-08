using Application.Models;
using Application.Models.CreateDTO;

namespace Application.Interfaces
{
    public interface IProcesoService
    {
        Task<IEnumerable<ProcesoDTO>> GetAllAsync();
        Task<ProcesoDTO?> GetByIdAsync(int id);
        Task<ProcesoDTO> CreateAsync(ProcesoCreateDTO dto);
        Task StartProcesoAsync(int id);
        Task EndProcesoAsync(int id);
        Task DeleteAsync(int id);
    }
}