using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Race;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Formula1InfoMVC.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceApiService _raceService;

        public RacesController(IRaceApiService raceService)
        {
            _raceService = raceService;
        }

        public async Task<IActionResult> Index(string? name, bool? isFuture, int page = 1)
        {
            int pageSize = 5;
            var result = await _raceService.GetAllAsync(name, isFuture, page, pageSize);

            ViewBag.SearchName = name;
            ViewBag.IsFuture = isFuture?.ToString().ToLower();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Items);
        }


        public async Task<IActionResult> Details(string id)
        {
            var race = await _raceService.GetByIdAsync(id);
            if (race == null) return NotFound();

            race.Drivers = await _raceService.GetDriverOptionsAsync(); // ✅ добавено

            return View(race);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var model = new RaceFormViewModel
            {
                Drivers = await _raceService.GetDriverOptionsAsync()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RaceFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Drivers = await _raceService.GetDriverOptionsAsync();
                return View(model);
            }

            var result = await _raceService.CreateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Could not create race.");
                model.Drivers = await _raceService.GetDriverOptionsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _raceService.GetByIdAsync(id);
            if (model == null) return NotFound();

            model.Drivers = await _raceService.GetDriverOptionsAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(RaceFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Drivers = await _raceService.GetDriverOptionsAsync();
                return View(model);
            }

            var result = await _raceService.UpdateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Could not update race.");
                model.Drivers = await _raceService.GetDriverOptionsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _raceService.DeleteAsync(id);
            TempData[result ? "Success" : "Error"] = result
                ? "Race deleted successfully."
                : "Could not delete race.";
            return RedirectToAction(nameof(Index));
        }
    }
}
