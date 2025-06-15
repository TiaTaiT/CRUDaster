using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var item = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            var createdItem = await _categoryRepository.AddAsync(item);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.Description
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _categoryRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _categoryRepository.DeleteAsync(toDelete);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var allItems = await _categoryRepository.GetAllAsync();
            return allItems.Select(f => new CategoryDto(
                Id: f.Id,
                Name: f.Name,
                Description: f.Description
            ));
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new CategoryDto(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description
            );
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var existingItem = await _categoryRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Item with ID {dto.Id} not found.");
            existingItem.Name = dto.Name ?? existingItem.Name;
            existingItem.Name = dto.Description ?? existingItem.Description;

            await _categoryRepository.UpdateAsync(existingItem);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
