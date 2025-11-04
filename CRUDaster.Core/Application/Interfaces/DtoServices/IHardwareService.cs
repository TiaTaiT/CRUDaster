using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IHardwareService : IEntityService<HardwareDto>
    {
        Task<HardwareDto?> GetByIdAsync(int id);
        Task<HardwareDto?> GetBySerialAsync(string serial);
        Task<HardwareDto> CreateAsync(HardwareCreateDto productDto);
        Task UpdateAsync(HardwareUpdateDto productDto);
        bool IsCapsuleAllowed(HardwareDto dto);
    }
}
