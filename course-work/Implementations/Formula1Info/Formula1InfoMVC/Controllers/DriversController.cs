using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Driver;
using Microsoft.AspNetCore.Mvc;

namespace Formula1InfoMVC.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverApiService _driverService;

        public DriversController(IDriverApiService driverService)
        {
            _driverService = driverService;
        }

        public async Task<IActionResult> Index(string? nationality, string? fullName, string? sort, int page = 1)
        {
            int pageSize = 5;
            var result = await _driverService.GetFilteredAsync(nationality, fullName, sort, page, pageSize);

            ViewBag.Nationality = nationality;
            ViewBag.FullName = fullName;
            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Items);
        }


        public async Task<IActionResult> Details(string id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null) return NotFound();
            return View(driver);
        }

        public async Task<IActionResult> Create()
        {
            var model = new DriverFormViewModel
            {
                Teams = await _driverService.GetTeamOptionsAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = await _driverService.GetTeamOptionsAsync();
                return View(model);
            }

            var result = await _driverService.CreateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Could not create driver.");
                model.Teams = await _driverService.GetTeamOptionsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null) return NotFound();

            driver.Teams = await _driverService.GetTeamOptionsAsync();
            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DriverFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = await _driverService.GetTeamOptionsAsync();
                return View(model);
            }

            var result = await _driverService.UpdateAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Could not update driver.");
                model.Teams = await _driverService.GetTeamOptionsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _driverService.DeleteAsync(id);

            if (!result)
            {
                TempData["Error"] = "Could not delete driver.";
            }
            else
            {
                TempData["Success"] = "Driver deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
