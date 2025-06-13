using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IBrandService : IEntityService<BrandDto>
    {
        Task<BrandDto?> GetByIdAsync(int id);
        Task<BrandDto> CreateAsync(BrandCreateDto dto);
        Task UpdateAsync(BrandUpdateDto dto);
    }
}
