using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VuDucNam_L1.Models;
using VuDucNam_L1.Service.IServices;
using VuDucNam_L1.Service.Services;

namespace VuDucNam_L1.Controllers
{
    public class WardController : Controller
    {
        private readonly IWardService _wardService;
        private readonly IDistrictService _districtService;

        public WardController(IWardService wardService, IDistrictService districtService)
        {
            _wardService = wardService;
            _districtService = districtService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var pageSize = 10;
            var wards = await _wardService.GetAllByPageAsync(pageNumber, pageSize);
            var totalCount = await _wardService.GetTotalCountAsync();
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return View(wards);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Districts = new SelectList(await _districtService.GetAllDistrictAsync(), "DistrictId", "DistrictName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WardModel wardModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _wardService.AddAsync(wardModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    foreach (var error in ex.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Districts = new SelectList(await _districtService.GetAllDistrictAsync(), "DistrictId", "DistrictName");
            return View(wardModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var wardModel = await _wardService.GetByIdAsync(id);
            if (wardModel == null)
            {
                return NotFound();
            }
            ViewBag.Districts = new SelectList(await _districtService.GetAllDistrictAsync(), "DistrictId", "DistrictName");

            return View(wardModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WardModel wardModel)
        {
            if (id != wardModel.WardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _wardService.UpdateAsync(wardModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    foreach (var error in ex.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Districts = new SelectList(await _districtService.GetAllDistrictAsync(), "DistrictId", "DistrictName");
            return View(wardModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var wardModel = await _wardService.GetByIdAsync(id);
            if (wardModel == null)
            {
                return NotFound();
            }

            return View(wardModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _wardService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var wardModel = await _wardService.GetByIdAsync(id);
                return View(wardModel);
            }
        }
    }
}
