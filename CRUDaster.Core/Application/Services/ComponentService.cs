using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Services
{
    public class ComponentService(
        IComponentRepository componentRepository,
        IStatusRepository statusRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository,
        IModelRepository modelRepository,
        IPimRepository pimRepository,
        IProtocolRepository protocolRepository,
        IUserContextService userContextService) : IComponentService
    {
        private readonly IComponentRepository _componentRepository = componentRepository;
        private readonly IStatusRepository _statusRepository = statusRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IBrandRepository _brandRepository = brandRepository;
        private readonly IModelRepository _modelRepository = modelRepository;
        private readonly IPimRepository _pimRepository = pimRepository;
        private readonly IProtocolRepository _protocolRepository = protocolRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<ComponentDto> CreateAsync(ComponentCreateDto dto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var status = await _statusRepository.GetByIdAsync(dto.StatusId) ?? throw new Exception("Status ID not found");
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId) ?? throw new Exception("Category ID not found");
            var brand = await _brandRepository.GetByIdAsync(dto.BrandId) ?? throw new Exception("Brand ID not found");
            
            var model = dto.ModelId != 0 ? await _modelRepository.GetByIdAsync((int)dto.ModelId) ?? throw new Exception("Model ID not found") : null;
            var pim = dto.PimId != 0 ? await _pimRepository.GetByIdAsync((int)dto.PimId) ?? throw new Exception("P&M ID not found") : null;

            var protocols = (await _protocolRepository.GetAllAsync())
                .Where(x => dto.ProtocolIds.Contains(x.Id))
                .ToList();

            var item = new Component
            {
                Name = dto.Name,
                AlterName = dto.AlterName,
                Description = dto.Description,
                VendorCode = dto.VendorCode,
                CanHasChildren = dto.CanHasChildren,
                Virtual = dto.Virtual,
                ErpCode = dto.ErpCode,
                Length = dto.Length,
                Width = dto.Width,
                Height = dto.Height,
                Status = status,
                Category = category,
                Brand = brand,
                Model = model,
                Pim = pim,
                Protocols = protocols,
                HasSerial = dto.HasSerial,
                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdItem = await _componentRepository.AddAsync(item);
            await _componentRepository.SaveChangesAsync();

            var simpleProtocolDtos = new List<ProtocolSimpleDto>();
            foreach (var protocol in protocols)
            {
                simpleProtocolDtos.Add(new ProtocolSimpleDto(protocol.Id, protocol.Name));
            }

            return new ComponentDto
            (
                createdItem.Id,
                createdItem.Name,
                createdItem.AlterName,
                createdItem.Description,
                createdItem.VendorCode,
                createdItem.CanHasChildren,
                createdItem.Virtual,
                createdItem.ErpCode,
                createdItem.Length,
                createdItem.Width,
                createdItem.Height,
                createdItem.Status,
                createdItem.Category,
                createdItem.Brand,
                createdItem.Model,
                createdItem.Pim,
                simpleProtocolDtos,
                createdItem.HasSerial,
                createdItem.CreatorId,
                createdItem.CreatedAt,
                createdItem.UpdaterId,
                createdItem.UpdatedAt
            );
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _componentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Component with ID {id} not found.");
            await _componentRepository.DeleteAsync(toDelete);
            await _componentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ComponentDto>> GetAllAsync()
        {
            // Assumes repository returns Functionality entities with their Hardware navigation loaded
            var allComponents = await _componentRepository.GetAllAsync();

            return allComponents.Select(f => new ComponentDto(
                Id: f.Id,
                Name: f.Name,
                AlterName: f.AlterName,
                Description: f.Description,
                VendorCode: f.VendorCode,
                CanHasChildren: f.CanHasChildren,
                Virtual: f.Virtual,
                ErpCode: f.ErpCode,
                Length: f.Length,
                Width: f.Width,
                Height: f.Height,
                Status: f.Status,
                Category: f.Category,
                Brand: f.Brand,
                Model: f.Model,
                Pim: f.Pim,
                ProtocolDtos: [.. f.Protocols.Select(h => new ProtocolSimpleDto(h.Id, h.Name))],
                HasSerial: f.HasSerial,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdatedAt: f.UpdatedAt,
                UpdaterId: f.UpdaterId
            ));
        }

        public async Task<ComponentDto?> GetByIdAsync(int id)
        {
            var f = await _componentRepository.GetByIdAsync(id);
            if (f == null) return null;

            return new ComponentDto(
                Id: f.Id,
                Name: f.Name,
                AlterName: f.AlterName,
                Description: f.Description,
                VendorCode: f.VendorCode,
                CanHasChildren: f.CanHasChildren,
                Virtual: f.Virtual,
                ErpCode: f.ErpCode,
                Length: f.Length,
                Width: f.Width,
                Height: f.Height,
                Status: f.Status,
                Category: f.Category,
                Brand: f.Brand,
                Model: f.Model,
                Pim: f.Pim,
                ProtocolDtos: [.. f.Protocols.Select(h => new ProtocolSimpleDto(h.Id, h.Name))],
                HasSerial: f.HasSerial,
                CreatorId: f.CreatorId,
                CreatedAt: f.CreatedAt,
                UpdatedAt: f.UpdatedAt,
                UpdaterId: f.UpdaterId
            );
        }

        public async Task UpdateAsync(ComponentUpdateDto dto)
        {
            var existingComponent = await _componentRepository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Protocol with ID {dto.Id} not found.");
            if (!string.IsNullOrEmpty(dto.Name))
            {
                existingComponent.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                existingComponent.Description = dto.Description;
            }

            if (dto.ProtocolIds != null)
            {
                existingComponent.Protocols.Clear();
                foreach (var protocolId in dto.ProtocolIds.Distinct())
                {
                    var protocol = await _protocolRepository.GetByIdAsync(protocolId)
                        ?? throw new KeyNotFoundException($"Protocol with ID {protocolId} not found.");
                    existingComponent.Protocols.Add(protocol);
                }
            }

            await _componentRepository.UpdateAsync(existingComponent);
            await _componentRepository.SaveChangesAsync();
        }
    }
}
