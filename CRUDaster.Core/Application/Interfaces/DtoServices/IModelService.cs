using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IModelService : IEntityService<ModelDto>
    {
        Task<ModelDto?> GetByIdAsync(int id);
        Task<ModelDto> CreateAsync(ModelCreateDto dto);
        Task UpdateAsync(ModelUpdateDto dto);
    }
}
