using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class PimController(IPimService pimService) : ControllerBase
    {
        private readonly IPimService _pimService = pimService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PimDto>>> GetItems()
        {
            var items = await _pimService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PimDto>> GetItem(int id)
        {
            var item = await _pimService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PimDto>> CreateItem(PimCreateDto dto)
        {
            var createdItem = await _pimService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, PimUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _pimService.UpdateAsync(dto);
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
                await _pimService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
