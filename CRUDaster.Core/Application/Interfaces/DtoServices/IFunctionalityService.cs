using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IFunctionalityService : IEntityService<FunctionalityDto>
    {
        Task<FunctionalityDto?> GetByIdAsync(int id);
        Task<FunctionalityDto> CreateAsync(FunctionalityCreateDto productDto);
        Task UpdateAsync(FunctionalityUpdateDto productDto);
    }
}
