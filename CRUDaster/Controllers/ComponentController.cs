using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController(IComponentService componentService) : ControllerBase
    {
        private readonly IComponentService _componentService = componentService;
        // GET: api/component
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetAll()
        {
            var components = await _componentService.GetAllAsync();
            return Ok(components);
        }

        // GET: api/component/fortables
        [AllowAnonymous]
        [HttpGet("fortables")]
        public async Task<ActionResult<IEnumerable<ComponentForTablesDto>>> GetAllForTables() 
        {
            var components = await _componentService.GetAllForTablesAsync();
            return Ok(components);
        }

        // GET: api/component/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetById(int id)
        {
            var component = await _componentService.GetByIdAsync(id);
            if (component == null)
                return NotFound();
            return Ok(component);
        }

        // POST: api/component
        [HttpPost]
        public async Task<ActionResult<ComponentDto>> Create(ComponentCreateDto dto)
        {
            var created = await _componentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/component/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ComponentUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            await _componentService.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/component/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _componentService.DeleteAsync(id);
            return NoContent();
        }
    }
}

