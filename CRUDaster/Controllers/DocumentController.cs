using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController(IDocumentService documentService) : ControllerBase
    {
        private readonly IDocumentService _documentService = documentService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetItems()
        {
            var items = await _documentService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetItem(int id)
        {
            var item = await _documentService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateItem(DocumentCreateDto dto)
        {
            var createdItem = await _documentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, DocumentUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _documentService.UpdateAsync(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _documentService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
