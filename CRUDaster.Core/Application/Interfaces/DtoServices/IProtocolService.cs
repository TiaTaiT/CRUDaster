using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IProtocolService : IEntityService<ProtocolDto>
    {
        Task<ProtocolDto?> GetByIdAsync(int id);
        Task<ProtocolDto> CreateAsync(ProtocolCreateDto dto);
        Task UpdateAsync(ProtocolUpdateDto dto);
    }
}
