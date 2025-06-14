﻿using CRUDaster.Core.Application.DTOs;
using CRUDaster.Core.Application.Interfaces.DtoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDaster.Controllers
{
    [Authorize(Roles = "ADMIN,PNR_EDITOR")]
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController(IHardwareService hardwareService) : ControllerBase
    {
        private readonly IHardwareService _hardwareService = hardwareService;

        // GET: api/hardware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HardwareDto>>> GetAll()
        {
            var hardwares = await _hardwareService.GetAllAsync();
            return Ok(hardwares);
        }

        // GET: api/hardware/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HardwareDto>> GetById(int id)
        {
            var hardware = await _hardwareService.GetByIdAsync(id);
            if (hardware == null)
                return NotFound();
            return Ok(hardware);
        }

        // GET: api/hardware/byserial/{serial}
        [AllowAnonymous]
        [HttpGet("byserial/{serial}")]
        public async Task<ActionResult<HardwareDto>> GetByName(string serial)
        {
            var hardware = await _hardwareService.GetBySerialAsync(serial);
            if (hardware == null)
                return NotFound();
            return Ok(hardware);
        }

        // POST: api/hardware
        [HttpPost]
        public async Task<ActionResult<HardwareDto>> Create(HardwareCreateDto dto)
        {
            var created = await _hardwareService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/hardware/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HardwareUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            await _hardwareService.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/hardware/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hardwareService.DeleteAsync(id);
            return NoContent();
        }
    }
}
