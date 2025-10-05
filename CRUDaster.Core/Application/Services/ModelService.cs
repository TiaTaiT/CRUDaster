using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class ModelService(IModelRepository modelRepository, IUserContextService userContextService) : IModelService
    {
        private readonly IUserContextService _userContextService = userContextService;
        private readonly IModelRepository _modelRepository = modelRepository;
        public async Task<ModelDto> CreateAsync(ModelCreateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var item = new Model
            {
                Name = dto.Name,
                Description = dto.Description,
                Model2dFile = dto.Model2dDFile,
                Model3dFile = dto.Model3dFile,
                ImageFile = dto.ImageFile,

                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdItem = await _modelRepository.AddAsync(item);
            await _modelRepository.SaveChangesAsync();

            return new ModelDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.Description,
                createdItem.Model2dFile,
                createdItem.Model3dFile,
                createdItem.ImageFile,
                createdItem.CreatorId,
                createdItem.CreatedAt,
                createdItem.UpdaterId,
                createdItem.UpdatedAt
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _modelRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _modelRepository.DeleteAsync(toDelete);
            await _modelRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ModelDto>> GetAllAsync()
        {
            var allItems = await _modelRepository.GetAllAsync();

            return allItems.Select(f => new ModelDto(
                Id: f.Id,
                Name: f.Name,
                Description: f.Description,
                Model2dFile: f.Model2dFile,
                Model3dFile: f.Model3dFile,
                ImageFile: f.ImageFile,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdaterId: f.UpdaterId,
                UpdatedAt: f.UpdatedAt
            ));
        }

        public async Task<ModelDto?> GetByIdAsync(int id)
        {
            var item = await _modelRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new ModelDto(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                Model2dFile: item.Model2dFile,
                Model3dFile: item.Model3dFile,
                ImageFile: item.ImageFile,
                CreatorId: item.CreatorId,
                CreatedAt: item.CreatedAt,
                UpdaterId: item.UpdaterId,
                UpdatedAt: item.UpdatedAt
            );
        }

        public async Task UpdateAsync(ModelUpdateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var existingItem = await _modelRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Product with ID {dto.Id} not found.");
            existingItem.Name = dto.Name;

            existingItem.UpdaterId = userId;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _modelRepository.UpdateAsync(existingItem);
            await _modelRepository.SaveChangesAsync();
        }
    }
}
