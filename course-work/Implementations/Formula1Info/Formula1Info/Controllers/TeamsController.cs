using Formula1Info.DTOs.TeamDTOs;
using Formula1Info.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Formula1Info.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/teams?name=Mercedes&country=UK&page=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name,
            [FromQuery] string? country,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _teamService.GetAllAsync(name, country, page, pageSize);
            return Ok(result);
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        // POST: api/teams
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TeamCreateDto dto)
        {
            var created = await _teamService.CreateAsync(dto);
            if (!created)
                return BadRequest("Failed to create team");

            return Ok("Team created successfully");
        }

        // PUT: api/teams/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, [FromBody] TeamUpdateDto dto)
        {
            if (id != dto.TeamId)
                return BadRequest("Team ID mismatch");

            var updated = await _teamService.UpdateAsync(dto);
            if (!updated)
                return NotFound("Team not found");

            return Ok("Team updated successfully");
        }

        // DELETE: api/teams/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _teamService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Team not found");

            return Ok("Team deleted successfully");
        }
    }
}
