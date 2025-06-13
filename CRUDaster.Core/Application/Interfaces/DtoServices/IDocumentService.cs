using CRUDaster.Core.Application.DTOs;

namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IDocumentService : IEntityService<DocumentDto>
    {
        Task<DocumentDto?> GetByIdAsync(int id);
        Task<DocumentDto> CreateAsync(DocumentCreateDto dto);
        Task UpdateAsync(DocumentUpdateDto dto);
    }
}
