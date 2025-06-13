using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IDraftService
    {
        Task<IEnumerable<DraftDto>> GetAllAsync();
        Task<DraftDto?> GetByIdAsync(int id);
        Task<DraftDto> CreateAsync(CreateDraftDto productDto);
        Task UpdateAsync(UpdateDraftDto productDto);
        Task DeleteAsync(int id);
    }
}
