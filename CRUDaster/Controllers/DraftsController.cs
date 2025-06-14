using Microsoft.AspNetCore.Mvc;
using CRUDaster.Core.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using CRUDaster.Core.Application.Interfaces.DtoServices;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class DraftsController(IDraftService draftService) : ControllerBase
    {
        private readonly IDraftService _draftService = draftService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DraftDto>>> GetDrafts()
        {
            var drafts = await _draftService.GetAllAsync();
            return Ok(drafts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DraftDto>> GetDraft(int id)
        {
            var draft = await _draftService.GetByIdAsync(id);

            if (draft == null)
            {
                return NotFound();
            }

            return Ok(draft);
        }

        [HttpPost]
        public async Task<ActionResult<DraftDto>> CreateDraft(CreateDraftDto draftDto)
        {
            var createdDraft = await _draftService.CreateAsync(draftDto);
            return CreatedAtAction(nameof(GetDraft), new { id = createdDraft.Id }, createdDraft);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDraft(int id, UpdateDraftDto draftDto)
        {
            if (id != draftDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _draftService.UpdateAsync(draftDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDraft(int id)
        {
            try
            {
                await _draftService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
