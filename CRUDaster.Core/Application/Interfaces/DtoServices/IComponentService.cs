using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IComponentService : IEntityService<ComponentDto>
    {
        Task<ComponentDto?> GetByIdAsync(int id);
        Task<ComponentDto> CreateAsync(ComponentCreateDto dto);
        Task UpdateAsync(ComponentUpdateDto dto);
        Task<IEnumerable<ComponentForTablesDto>> GetAllForTablesAsync();
    }
}
