using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;
using CRUDaster.Core.Application.Interfaces.DtoServices;

namespace CRUDaster.Core.Application.Services
{
    public class DraftService(IDraftRepository draftRepository, IUserContextService userContextService) : IDraftService
    {
        private readonly IDraftRepository _draftRepository = draftRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<IEnumerable<DraftDto>> GetAllAsync()
        {
            var drafts = await _draftRepository.GetAllAsync();
            return drafts.Select(draft => new DraftDto
            {
                Id = draft.Id,
                Description = draft.Description,
                Model3Dfile = draft.Model3Dfile,
                Model2Dfile = draft.Model2Dfile,
                ImageFile = draft.ImageFile,

                CreatorId = draft.CreatorId,
                CreatedAt = draft.CreatedAt,
                UpdaterId = draft.UpdaterId,
                UpdatedAt = draft.UpdatedAt,
            });
        }

        public async Task<DraftDto?> GetByIdAsync(int id)
        {
            var draft = await _draftRepository.GetByIdAsync(id);
            if (draft == null) return null;

            return new DraftDto
            {
                Id = draft.Id,
                Description = draft.Description,
                Model3Dfile = draft.Model3Dfile,
                Model2Dfile = draft.Model2Dfile,
                ImageFile = draft.ImageFile,

                CreatorId = draft.CreatorId,
                CreatedAt = draft.CreatedAt,
                UpdaterId = draft.UpdaterId,
                UpdatedAt = draft.UpdatedAt,
            };
        }

        public async Task<DraftDto> CreateAsync(CreateDraftDto draftDto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var draft = new Draft
            {
                Description = draftDto.Description,
                Model3Dfile = draftDto.Model3Dfile,
                Model2Dfile = draftDto.Model2Dfile,
                ImageFile = draftDto.ImageFile,

                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdDraft = await _draftRepository.AddAsync(draft);
            await _draftRepository.SaveChangesAsync();

            return new DraftDto
            {
                Id = createdDraft.Id,
                Description = createdDraft.Description,
                Model3Dfile = createdDraft.Model3Dfile,
                Model2Dfile = createdDraft.Model2Dfile,
                ImageFile = createdDraft.ImageFile,
                CreatedAt = createdDraft.CreatedAt,
                CreatorId = createdDraft.CreatorId,
            };
        }

        public async Task UpdateAsync(UpdateDraftDto draftDto)
        {
            var userId = _userContextService.GetUserId() ?? throw new Exception("User ID not found");
            var existingDraft = await _draftRepository.GetByIdAsync(draftDto.Id) ?? throw new KeyNotFoundException($"Product with ID {draftDto.Id} not found.");
            existingDraft.Description = draftDto.Description;
            existingDraft.Model3Dfile = draftDto.Model3Dfile;
            existingDraft.Model2Dfile = draftDto.Model2Dfile;
            existingDraft.ImageFile = draftDto.ImageFile;

            existingDraft.UpdaterId = userId;
            existingDraft.UpdatedAt = DateTime.UtcNow;

            await _draftRepository.UpdateAsync(existingDraft);
            await _draftRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var draft = await _draftRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            await _draftRepository.DeleteAsync(draft);
            await _draftRepository.SaveChangesAsync();
        }
    }
}
