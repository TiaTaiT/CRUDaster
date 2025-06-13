using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;
using CRUDaster.Core.Application.Interfaces.DtoServices;

namespace CRUDaster.Core.Application.Services
{
    public class DocumentService(IDocumentRepository documentRepository, IUserContextService userContextService) : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository = documentRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<DocumentDto> CreateAsync(DocumentCreateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var item = new Document
            {
                Name = dto.Name,

                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdItem = await _documentRepository.AddAsync(item);
            await _documentRepository.SaveChangesAsync();

            return new DocumentDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.CreatorId,
                createdItem.CreatedAt,
                createdItem.UpdaterId,
                createdItem.UpdatedAt
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _documentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _documentRepository.DeleteAsync(toDelete);
            await _documentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var allItems = await _documentRepository.GetAllAsync();

            return allItems.Select(f => new DocumentDto(
                Id: f.Id,
                Name: f.Name,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdaterId: f.UpdaterId,
                UpdatedAt: f.UpdatedAt
            ));
        }

        public async Task<DocumentDto?> GetByIdAsync(int id)
        {
            var item = await _documentRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new DocumentDto(
                Id: item.Id,
                Name: item.Name,
                CreatorId: item.CreatorId,
                CreatedAt: item.CreatedAt,
                UpdaterId: item.UpdaterId,
                UpdatedAt: item.UpdatedAt

            );
        }

        public async Task UpdateAsync(DocumentUpdateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var existingItem = await _documentRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Product with ID {dto.Id} not found.");
            existingItem.Name = dto.Name;

            existingItem.UpdaterId = userId;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _documentRepository.UpdateAsync(existingItem);
            await _documentRepository.SaveChangesAsync();
        }
    }
}
