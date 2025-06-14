using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetItems()
        {
            var items = await _brandService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetItem(int id)
        {
            var item = await _brandService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> CreateItem(BrandCreateDto dto)
        {
            var createdItem = await _brandService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, BrandUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _brandService.UpdateAsync(dto);
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
                await _brandService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
