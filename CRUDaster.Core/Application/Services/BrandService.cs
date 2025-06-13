using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class BrandService(IBrandRepository brandRepository, IUserContextService userContextService) : IBrandService
    {
        private readonly IBrandRepository _brandRepository = brandRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var allItems = await _brandRepository.GetAllAsync();

            return allItems.Select(f => new BrandDto(
                Id: f.Id,
                Name: f.Name,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdaterId: f.UpdaterId,
                UpdatedAt: f.UpdatedAt
            ));
        }

        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            var item = await _brandRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new BrandDto(
                Id: item.Id,
                Name: item.Name,
                CreatorId: item.CreatorId,
                CreatedAt: item.CreatedAt,
                UpdaterId: item.UpdaterId,
                UpdatedAt: item.UpdatedAt

            );
        }

        public async Task<BrandDto> CreateAsync(BrandCreateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var item = new Brand
            {
                Name = dto.Name,

                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdItem = await _brandRepository.AddAsync(item);
            await _brandRepository.SaveChangesAsync();

            return new BrandDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.CreatorId,
                createdItem.CreatedAt,
                createdItem.UpdaterId,
                createdItem.UpdatedAt
            );
        }

        public async Task UpdateAsync(BrandUpdateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var existingItem = await _brandRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Item with ID {dto.Id} not found.");
            existingItem.Name = dto.Name;

            existingItem.UpdaterId = userId;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _brandRepository.UpdateAsync(existingItem);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _brandRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _brandRepository.DeleteAsync(toDelete);
            await _brandRepository.SaveChangesAsync();
        }
    }
}
