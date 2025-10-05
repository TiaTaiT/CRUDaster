using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class ProtocolService(IProtocolRepository protocolRepository, IComponentRepository componentRepository) : IProtocolService
    {
        private readonly IProtocolRepository _protocolRepository = protocolRepository;
        private readonly IComponentRepository _componentRepository = componentRepository;
        public async Task<ProtocolDto> CreateAsync(ProtocolCreateDto dto)
        {
            var newProtocol = new Protocol
            {
                Name = dto.Name,
                Description = dto.Description,
                Components = [],
            };

            foreach (var componentId in dto.ComponentIds.Distinct())
            {
                var component = await _componentRepository.GetByIdAsync(componentId);
                if (component == null)
                {
                    throw new KeyNotFoundException($"Hardware with ID {componentId} not found.");
                }
                newProtocol.Components.Add(component);
            }

            var created = await _protocolRepository.AddAsync(newProtocol);
            await _protocolRepository.SaveChangesAsync();

            return new ProtocolDto(
                Id: created.Id,
                Name: created.Name,
                Description: created.Description,
                Components: created.Components
                    .Select(h => new ComponentSimpleDto(h.Id, h.Name))
                    .ToList()
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _protocolRepository.GetByIdAsync(id);
            if (toDelete == null)
            {
                throw new KeyNotFoundException($"Protocol with ID {id} not found.");
            }

            await _protocolRepository.DeleteAsync(toDelete);
            await _protocolRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProtocolDto>> GetAllAsync()
        {
            var allProtocols = await _protocolRepository.GetAllAsync();

            return allProtocols.Select(f => new ProtocolDto(
                Id: f.Id,
                Name: f.Name,
                Description: f.Description,
                Components: f.Components
                    .Select(h => new ComponentSimpleDto(h.Id, h.Name))
                    .ToList()
            ));
        }

        public async Task<ProtocolDto?> GetByIdAsync(int id)
        {
            var func = await _protocolRepository.GetByIdAsync(id);
            if (func == null) return null;

            return new ProtocolDto(
                Id: func.Id,
                Name: func.Name,
                Description: func.Description,
                Components: func.Components
                    .Select(h => new ComponentSimpleDto(h.Id, h.Name))
                    .ToList()
            );
        }

        public async Task UpdateAsync(ProtocolUpdateDto dto)
        {
            var existingProtocol = await _protocolRepository.GetByIdAsync(dto.Id);
            if (existingProtocol == null)
            {
                throw new KeyNotFoundException($"Protocol with ID {dto.Id} not found.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                existingProtocol.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                existingProtocol.Description = dto.Description;
            }

            if (dto.ComponentIds != null)
            {
                existingProtocol.Components.Clear();
                foreach (var componentId in dto.ComponentIds.Distinct())
                {
                    var component = await _componentRepository.GetByIdAsync(componentId);
                    if (component == null)
                    {
                        throw new KeyNotFoundException($"Component with ID {componentId} not found.");
                    }
                    existingProtocol.Components.Add(component);
                }
            }

            await _protocolRepository.UpdateAsync(existingProtocol);
            await _protocolRepository.SaveChangesAsync();
        }
    }
}
