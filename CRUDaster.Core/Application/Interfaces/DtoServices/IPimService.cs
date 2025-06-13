using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IPimService : IEntityService<PimDto>
    {
        Task<PimDto?> GetByIdAsync(int id);
        Task<PimDto> CreateAsync(PimCreateDto dto);
        Task UpdateAsync(PimUpdateDto dto);
    }
}
