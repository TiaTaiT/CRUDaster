using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities.AppUserRights;

namespace CRUDaster.Core.Application.Services
{
    public class HardwareService(
        IHardwareRepository hardwareRepository,
        IFunctionalityRepository functionalityRepository) : IHardwareService
    {
        private readonly IHardwareRepository _hardwareRepository = hardwareRepository;
        private readonly IFunctionalityRepository _functionalityRepository = functionalityRepository;

        public async Task<IEnumerable<HardwareDto>> GetAllAsync()
        {
            // Assumes the repository returns Hardware entities with their Functionalities loaded.
            var allHardware = await _hardwareRepository.GetAllAsync();

            return allHardware.Select(h => new HardwareDto(
                Id: h.Id,
                Serial: h.Serial,
                Description: h.Description,
                Functionalities: [.. h.Functionalities.Select(f => new FunctionalitySimpleDto(f.Id, f.Name))]
            ));
        }

        public async Task<HardwareDto?> GetByIdAsync(int id)
        {
            // Assumes GetByIdAsync includes the Functionalities navigation.
            var hw = await _hardwareRepository.GetByIdAsync(id);
            if (hw == null) return null;

            return new HardwareDto(
                Id: hw.Id,
                Serial: hw.Serial,
                Description: hw.Description,
                Functionalities: [.. hw.Functionalities.Select(f => new FunctionalitySimpleDto(f.Id, f.Name))]
            );
        }

        public async Task<HardwareDto?> GetBySerialAsync(string serial)
        {
            var hw = await _hardwareRepository.GetBySerialAsync(serial);
            if (hw == null) return null;

            return new HardwareDto(
                Id: hw.Id,
                Serial: hw.Serial,
                Description: hw.Description,
                Functionalities: [.. hw.Functionalities.Select(f => new FunctionalitySimpleDto(f.Id, f.Name))]
            );
        }

        public async Task<HardwareDto> CreateAsync(HardwareCreateDto dto)
        {
            // 1) Create the new Hardware entity
            var newHw = new Hardware
            {
                Serial = dto.Serial,
                Description = dto.Description,
                Functionalities = []
            };

            // 2) For each requested functionality‐ID, fetch from the functionalityRepository
            foreach (var funcId in dto.FunctionalityIds.Distinct())
            {
                var func = await _functionalityRepository.GetByIdAsync(funcId) ?? throw new KeyNotFoundException($"Functionality with ID {funcId} not found.");
                newHw.Functionalities.Add(func);
            }

            // 3) Save
            var created = await _hardwareRepository.AddAsync(newHw);
            await _hardwareRepository.SaveChangesAsync();

            // 4) Map back to DTO
            return new HardwareDto(
                Id: created.Id,
                Serial: created.Serial,
                Description: created.Description,
                Functionalities: [.. created.Functionalities.Select(f => new FunctionalitySimpleDto(f.Id, f.Name))]
            );
        }

        public async Task UpdateAsync(HardwareUpdateDto dto)
        {
            // 1) Load existing Hardware (with its Functionalities)
            var existingHw = await _hardwareRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Hardware with ID {dto.Id} not found.");

            // 2) Patch scalar properties if provided
            if (!string.IsNullOrEmpty(dto.Serial))
            {
                existingHw.Serial = dto.Serial;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                existingHw.Description = dto.Description;
            }

            // 3) If the client passed a new list of FunctionalityIds, re‐assign the join set
            if (dto.FunctionalityIds != null)
            {
                // Clear current links
                existingHw.Functionalities.Clear();

                // Attach new requested set
                foreach (var funcId in dto.FunctionalityIds.Distinct())
                {
                    var func = await _functionalityRepository.GetByIdAsync(funcId) ?? throw new KeyNotFoundException($"Functionality with ID {funcId} not found.");
                    existingHw.Functionalities.Add(func);
                }
            }

            // 4) Persist
            await _hardwareRepository.UpdateAsync(existingHw);
            await _hardwareRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _hardwareRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Hardware with ID {id} not found.");
            await _hardwareRepository.DeleteAsync(toDelete);
            await _hardwareRepository.SaveChangesAsync();
        }

        public bool IsCapsuleAllowed(HardwareDto dto)
        {
            foreach (var feature in dto.Functionalities)
            {
                if (feature.Name.Equals("GOOGLEAPPACCESS"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}