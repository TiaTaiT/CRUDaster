using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolController(IProtocolService protocolService) : ControllerBase
    {
        private readonly IProtocolService _protocolService = protocolService;
        // GET: api/protocol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProtocolDto>>> GetAll()
        {
            var protocol = await _protocolService.GetAllAsync();
            return Ok(protocol);
        }

        // GET: api/protocol/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProtocolDto>> GetById(int id)
        {
            var protocol = await _protocolService.GetByIdAsync(id);
            if (protocol == null)
                return NotFound();
            return Ok(protocol);
        }

        // POST: api/protocol
        [HttpPost]
        public async Task<ActionResult<ProtocolDto>> Create(ProtocolCreateDto dto)
        {
            var created = await _protocolService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/protocol/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProtocolUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            await _protocolService.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/protocol/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _protocolService.DeleteAsync(id);
            return NoContent();
        }
    }
}
