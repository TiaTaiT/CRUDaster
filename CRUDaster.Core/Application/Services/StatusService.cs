using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;

        public async Task<StatusDto> CreateAsync(StatusCreateDto dto)
        {
            var item = new Status
            {
                Name = dto.Name,
            };

            var createdItem = await _statusRepository.AddAsync(item);
            await _statusRepository.SaveChangesAsync();

            return new StatusDto
            (
                createdItem.Id,
                createdItem.Name
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _statusRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            await _statusRepository.DeleteAsync(toDelete);
            await _statusRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            var allItems = await _statusRepository.GetAllAsync();

            return allItems.Select(f => new StatusDto(
                Id: f.Id,
                Name: f.Name
            ));
        }

        public async Task<StatusDto?> GetByIdAsync(int id)
        {
            var item = await _statusRepository.GetByIdAsync(id);
            if (item == null) return null;

            return new StatusDto(
                Id: item.Id,
                Name: item.Name
            );
        }

        public async Task UpdateAsync(StatusUpdateDto dto)
        {
            var existingItem = await _statusRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Item with ID {dto.Id} not found.");
            existingItem.Name = dto.Name ?? existingItem.Name;

            await _statusRepository.UpdateAsync(existingItem);
            await _statusRepository.SaveChangesAsync();
        }
    }
}
