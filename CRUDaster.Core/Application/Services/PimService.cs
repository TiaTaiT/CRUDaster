using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;
using CRUDaster.Core.Application.Interfaces.DtoServices;

namespace CRUDaster.Core.Application.Services
{
    public class PimService(IPimRepository pimRepository, IUserContextService userContextService) : IPimService
    {
        private readonly IPimRepository _pimRepository = pimRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<PimDto> CreateAsync(PimCreateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var item = new Pim
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
                Content = dto.Content,

                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdItem = await _pimRepository.AddAsync(item);
            await _pimRepository.SaveChangesAsync();

            return new PimDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.Description,
                createdItem.Content,
                createdItem.CreatorId,
                createdItem.CreatedAt,
                createdItem.UpdaterId,
                createdItem.UpdatedAt
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _pimRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _pimRepository.DeleteAsync(toDelete);
            await _pimRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PimDto>> GetAllAsync()
        {
            var allItems = await _pimRepository.GetAllAsync();

            return allItems.Select(f => new PimDto(
                Id: f.Id,
                Name: f.Name,
                Description: f.Description,
                Content: f.Content,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdaterId: f.UpdaterId,
                UpdatedAt: f.UpdatedAt
            ));
        }

        public async Task<PimDto?> GetByIdAsync(int id)
        {
            var item = await _pimRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new PimDto(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                Content: item.Content,
                CreatorId: item.CreatorId,
                CreatedAt: item.CreatedAt,
                UpdaterId: item.UpdaterId,
                UpdatedAt: item.UpdatedAt

            );
        }

        public async Task UpdateAsync(PimUpdateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var existingItem = await _pimRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Item with ID {dto.Id} not found.");
            if (dto.Name != null)
            {
                existingItem.Name = dto.Name;
            }
            if (dto.Description != null)
            {
                existingItem.Description = dto.Description;
            }
            if (dto.Content != null)
            {
                existingItem.Content = dto.Content;
            }
            existingItem.UpdaterId = userId;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _pimRepository.UpdateAsync(existingItem);
            await _pimRepository.SaveChangesAsync();
        }
    }
}
