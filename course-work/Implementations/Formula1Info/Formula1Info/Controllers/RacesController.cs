using Formula1Info.DTOs.RaceDTOs;
using Formula1Info.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Formula1Info.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        // GET: api/races?name=Monaco&isFuture=true&page=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name,
            [FromQuery] bool? isFuture,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _raceService.GetAllAsync(name, isFuture, page, pageSize);
            return Ok(result);
        }

        // GET: api/races/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var race = await _raceService.GetByIdAsync(id);
            if (race == null)
                return NotFound();

            return Ok(race);
        }

        // POST: api/races
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] RaceCreateDto dto)
        {
            var created = await _raceService.CreateAsync(dto);
            if (!created)
                return BadRequest("Failed to create race");

            return Ok("Race created successfully");
        }

        // PUT: api/races/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, [FromBody] RaceUpdateDto dto)
        {
            if (id != dto.RaceId)
                return BadRequest("Race ID mismatch");

            var updated = await _raceService.UpdateAsync(dto);
            if (!updated)
                return NotFound("Race not found");

            return Ok("Race updated successfully");
        }

        // DELETE: api/races/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _raceService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Race not found");

            return Ok("Race deleted successfully");
        }
    }
}
