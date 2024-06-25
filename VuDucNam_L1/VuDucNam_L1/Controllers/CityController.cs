using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VuDucNam_L1.Models;
using VuDucNam_L1.Service.IServices;
using VuDucNam_L1.Service;

namespace VuDucNam_L1.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private const int PageSize = 10;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var cities = await _cityService.GetAllAsync(pageNumber, pageSize);
            int totalCities = await _cityService.GetTotalCountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCities / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _cityService.AddAsync(cityModel);
                if (result.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View(cityModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cityModel = await _cityService.GetByIdAsync(id);
            if (cityModel == null)
            {
                return NotFound();
            }
            return View(cityModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CityModel cityModel)
        {
            if (id != cityModel.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _cityService.UpdateAsync(cityModel);
                if (result.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View(cityModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cityModel = await _cityService.GetByIdAsync(id);
            if (cityModel == null)
            {
                return NotFound();
            }
            return View(cityModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _cityService.DeleteAsync(id);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var cityModel = await _cityService.GetByIdAsync(id);
                return View("Delete", cityModel);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
