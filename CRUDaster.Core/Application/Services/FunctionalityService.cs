using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities.AppUserRights;
using CRUDaster.Core.Application.Interfaces.DtoServices;

namespace CRUDaster.Core.Application.Services
{
    public class FunctionalityService(
        IFunctionalityRepository functionalityRepository,
        IHardwareRepository hardwareRepository) : IFunctionalityService
    {
        private readonly IFunctionalityRepository _functionalityRepository = functionalityRepository;
        private readonly IHardwareRepository _hardwareRepository = hardwareRepository;

        public async Task<IEnumerable<FunctionalityDto>> GetAllAsync()
        {
            // Assumes repository returns Functionality entities with their Hardware navigation loaded
            var allFuncs = await _functionalityRepository.GetAllAsync();

            return allFuncs.Select(f => new FunctionalityDto(
                Id: f.Id,
                Name: f.Name,
                Description: f.Description,
                Hardwares: f.Hardwares
                    .Select(h => new HardwareSimpleDto(h.Id, h.Serial))
                    .ToList()
            ));
        }

        public async Task<FunctionalityDto?> GetByIdAsync(int id)
        {
            var func = await _functionalityRepository.GetByIdAsync(id);
            if (func == null) return null;

            return new FunctionalityDto(
                Id: func.Id,
                Name: func.Name,
                Description: func.Description,
                Hardwares: func.Hardwares
                    .Select(h => new HardwareSimpleDto(h.Id, h.Serial))
                    .ToList()
            );
        }

        public async Task<FunctionalityDto> CreateAsync(FunctionalityCreateDto dto)
        {
            var newFunc = new Functionality
            {
                Name = dto.Name,
                Description = dto.Description,
                Hardwares = [],
            };

            foreach (var hwId in dto.HardwareIds.Distinct())
            {
                var hw = await _hardwareRepository.GetByIdAsync(hwId);
                if (hw == null)
                {
                    throw new KeyNotFoundException($"Hardware with ID {hwId} not found.");
                }
                newFunc.Hardwares.Add(hw);
            }

            var created = await _functionalityRepository.AddAsync(newFunc);
            await _functionalityRepository.SaveChangesAsync();

            return new FunctionalityDto(
                Id: created.Id,
                Name: created.Name,
                Description: created.Description,
                Hardwares: created.Hardwares
                    .Select(h => new HardwareSimpleDto(h.Id, h.Serial))
                    .ToList()
            );
        }

        public async Task UpdateAsync(FunctionalityUpdateDto dto)
        {
            var existingFunc = await _functionalityRepository.GetByIdAsync(dto.Id);
            if (existingFunc == null)
            {
                throw new KeyNotFoundException($"Functionality with ID {dto.Id} not found.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                existingFunc.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                existingFunc.Description = dto.Description;
            }

            if (dto.HardwareIds != null)
            {
                existingFunc.Hardwares.Clear();
                foreach (var hwId in dto.HardwareIds.Distinct())
                {
                    var hw = await _hardwareRepository.GetByIdAsync(hwId);
                    if (hw == null)
                    {
                        throw new KeyNotFoundException($"Hardware with ID {hwId} not found.");
                    }
                    existingFunc.Hardwares.Add(hw);
                }
            }

            await _functionalityRepository.UpdateAsync(existingFunc);
            await _functionalityRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _functionalityRepository.GetByIdAsync(id);
            if (toDelete == null)
            {
                throw new KeyNotFoundException($"Functionality with ID {id} not found.");
            }

            await _functionalityRepository.DeleteAsync(toDelete);
            await _functionalityRepository.SaveChangesAsync();
        }
    }
}
