using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalityController(IFunctionalityService functionalityService) : ControllerBase
    {
        private readonly IFunctionalityService _functionalityService = functionalityService;
        // GET: api/functionality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FunctionalityDto>>> GetAll()
        {
            var functionalities = await _functionalityService.GetAllAsync();
            return Ok(functionalities);
        }

        // GET: api/functionality/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FunctionalityDto>> GetById(int id)
        {
            var functionality = await _functionalityService.GetByIdAsync(id);
            if (functionality == null)
                return NotFound();
            return Ok(functionality);
        }

        // POST: api/functionality
        [HttpPost]
        public async Task<ActionResult<FunctionalityDto>> Create(FunctionalityCreateDto dto)
        {
            var created = await _functionalityService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/functionality/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FunctionalityUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            await _functionalityService.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/functionality/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _functionalityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
