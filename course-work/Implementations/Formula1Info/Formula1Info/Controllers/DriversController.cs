using Formula1Info.DTOs.DriverDTOs;
using Formula1Info.Services;
using Formula1Info.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Formula1Info.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET: api/drivers?nationality=British&fullName=Lewis&sort=championships
        [HttpGet]
        public async Task<IActionResult> GetAll(
                [FromQuery] string? nationality,
                [FromQuery] string? fullName,
                [FromQuery] string? sort,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10)
        {
            var result = await _driverService.GetAllAsync(nationality, fullName, sort, page, pageSize);
            return Ok(result); 
        }


        // GET: api/drivers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        // POST: api/drivers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DriverCreateDto dto)
        {
            var created = await _driverService.CreateAsync(dto);
            if (!created) return BadRequest("Failed to create driver");
            return Ok();
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, [FromBody] DriverUpdateDto dto)
        {
            if (id != dto.DriverId)
                return BadRequest("ID mismatch");

            var updated = await _driverService.UpdateAsync(dto);
            if (!updated) return NotFound();
            return Ok();
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _driverService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
