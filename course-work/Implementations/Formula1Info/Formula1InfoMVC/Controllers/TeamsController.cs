using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;

namespace Formula1InfoMVC.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamApiService _teamService;

        public TeamsController(ITeamApiService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index(string? searchName, string? searchCountry, int page = 1, int itemsPerPage = 5)
        {
            var result = await _teamService.GetAllAsync(searchName, searchCountry, page, itemsPerPage);

            ViewBag.SearchName = searchName;
            ViewBag.SearchCountry = searchCountry;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = result.TotalPages;
            ViewBag.ItemsPerPage = itemsPerPage;

            return View(result.Items);
        }


        public async Task<IActionResult> Details(string id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TeamFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _teamService.CreateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to create team.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _teamService.UpdateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to update team.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var result = await _teamService.DeleteAsync(id);
            if (!result) return BadRequest("Could not delete team.");

            return RedirectToAction(nameof(Index));
        }
    }
}
