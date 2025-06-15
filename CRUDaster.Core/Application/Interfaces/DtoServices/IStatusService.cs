using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IStatusService : IEntityService<StatusDto>
    {
        Task<StatusDto?> GetByIdAsync(int id);
        Task<StatusDto> CreateAsync(StatusCreateDto dto);
        Task UpdateAsync(StatusUpdateDto dto);
    }
}
