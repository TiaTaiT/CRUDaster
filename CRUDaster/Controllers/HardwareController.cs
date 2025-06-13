using CRUDaster.Core.Application.DTOs;
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
    }
}
