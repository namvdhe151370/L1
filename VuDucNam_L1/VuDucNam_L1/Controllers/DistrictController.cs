using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VuDucNam_L1.Models;
using VuDucNam_L1.Service.IServices;

namespace VuDucNam_L1.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;

        public DistrictController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 10;
            var districts = await _districtService.GetAllByPageAsync(pageNumber, pageSize);
            var totalCount = await _districtService.GetTotalCountAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(districts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DistrictModel district)
        {
            if (ModelState.IsValid)
            {
                await _districtService.AddAsync(district);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cities = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");
            return View(district);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            ViewBag.Cities = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");
            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DistrictModel district)
        {
            if (id != district.DistrictId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _districtService.UpdateAsync(district);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await DistrictExists(district.DistrictId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.Cities = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");
            return View(district);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _districtService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DistrictExists(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            return district != null;
        }
    }

}
