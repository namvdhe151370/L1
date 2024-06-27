using ExcelDataReader;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using VuDucNam_L1.Constants;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Service.IServices;
using VuDucNam_L1.Validation;

namespace VuDucNam_L1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        private readonly IJobService _jobService;
        private readonly IEthnicService _ethnicService;

        public EmployeeController(
            IEmployeeService employeeService,
            ICityService cityService,
            IDistrictService districtService,
            IWardService wardService,
            IJobService jobService,
            IEthnicService ethnicService)
        {
            _employeeService = employeeService;
            _cityService = cityService;
            _districtService = districtService;
            _wardService = wardService;
            _jobService = jobService;
            _ethnicService = ethnicService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);
            int totalEmployees = await _employeeService.GetTotalEmployeeCountAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Export(string selectedEmployeeIds)
        {
            if (string.IsNullOrEmpty(selectedEmployeeIds))
            {
                TempData[Validates.ErrorMessage] = Validates.NoEmployeesSelectedForExport;
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var employeeIds = selectedEmployeeIds.Split(',').Select(int.Parse).ToList();
                var employees = await _employeeService.GetEmployeesToExportAsync(employeeIds);
                var memoryStream = _employeeService.ExportEmployeesToCsv(employees);

                TempData[Validates.SuccessMessage] = Validates.EmployeesExportedSuccessfully;
                return File(memoryStream, "text/csv", "employees.xls");
            }
            catch (Exception ex)
            {
                TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorExportingEmployees, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ImportEmployees(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                TempData[Validates.ErrorMessage] = Validates.NoFileUploaded;
                return RedirectToAction(nameof(Index));
            }
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                TempData[Validates.ErrorMessage] = Validates.InvalidFileFormat;
                return RedirectToAction(nameof(Index));
            }
            try
            {
                var (employees, errors) = await _employeeService.ValidateAndParseExcelAsync(file);

                if (errors.Any())
                {
                    TempData[Validates.ErrorMessage] = string.Join(", ", errors);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var employee in employees)
                {
                    try
                    {
                        await _employeeService.CreateEmployeeToImportAsync(employee);
                    }
                    catch (FluentValidation.ValidationException ex)
                    {
                        foreach (var error in ex.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                        TempData[Validates.ErrorMessage] = Validates.ValidationErrorsOccurred;
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorCreatingEmployee, ex.Message);
                        return RedirectToAction(nameof(Index));
                    }
                }

                TempData[Validates.SuccessMessage] = Validates.EmployeesImportedSuccessfully;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorProcessingFile, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.EthnicList = new SelectList(await _ethnicService.GetAllEthnicAsync(), "EthnicId", "EthnicName");
            ViewBag.JobList = new SelectList(await _jobService.GetAllJobAsync(), "JobId", "JobName");
            ViewBag.CityList = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");
            ViewBag.DistrictList = new SelectList(new List<District>(), "DistrictId", "DistrictName");
            ViewBag.WardList = new SelectList(new List<Ward>(), "WardId", "WardName");
            var model = new EmployeeModel { Certificates = new List<CertificateModel>() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.AddEmployeeAsync(employeeModel);
                    await _employeeService.AddCertificatesAsync(employeeModel.EmployeeId, employeeModel.Certificates);
                    TempData[Validates.SuccessMessage] = Validates.EmployeeCreatedSuccessfully;
                    return RedirectToAction(nameof(Index));
                }
                catch (FluentValidation.ValidationException ex)
                {
                    TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorCreatingEmployee, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorCreatingEmployee, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            await PrepareDropDownLists(employeeModel);
            return View(employeeModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            await PrepareDropDownLists(model);
            var certificates = await _employeeService.GetCertificatesByEmployeeIdAsync(id.Value);
            model.Certificates = certificates.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeModel model)
        {
            if (id != model.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(model);
                    await _employeeService.UpdateCertificatesAsync(model);
                    TempData[Validates.SuccessMessage] = Validates.EmployeeUpdatedSuccessfully;
                    return RedirectToAction(nameof(Index));
                }
                catch (FluentValidation.ValidationException ex)
                {
                    TempData[Validates.ErrorMessage] = Validates.ValidationErrorsOccurred;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorUpdatingEmployee, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }

            await PrepareDropDownLists(model);
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            var certificates = await _employeeService.GetCertificatesByEmployeeIdAsync(id.Value);
            model.Certificates = certificates.ToList();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            var certificates = await _employeeService.GetCertificatesByEmployeeIdAsync(id.Value);
            model.Certificates = certificates.ToList();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                await _employeeService.DeleteCertificatesAsync(id);
                TempData[Validates.SuccessMessage] = Validates.EmployeeDeletedSuccessfully;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[Validates.ErrorMessage] = string.Format(Validates.ErrorDeletingEmployee, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // Endpoint to fetch districts based on cityId
        public async Task<IActionResult> GetDistricts(int cityId)
        {
            var districts = await _districtService.GetDistrictsByCityIdAsync(cityId);
            var districtList = districts.Select(d => new { d.DistrictId, d.DistrictName }).ToList();
            return Json(districtList);
        }

        // Endpoint to fetch wards based on districtId
        public async Task<IActionResult> GetWards(int districtId)
        {
            var wards = await _wardService.GetWardsByDistrictIdAsync(districtId);
            var wardList = wards.Select(w => new { w.WardId, w.WardName }).ToList();
            return Json(wardList);
        }

        private async Task PrepareDropDownLists(EmployeeModel employeeModel)
        {
            ViewBag.EthnicList = new SelectList(await _ethnicService.GetAllEthnicAsync(), "EthnicId", "EthnicName");
            ViewBag.JobList = new SelectList(await _jobService.GetAllJobAsync(), "JobId", "JobName");
            ViewBag.CityList = new SelectList(await _cityService.GetAllCities(), "CityId", "CityName");

            if (employeeModel.CityId != 0)
            {
                ViewBag.DistrictList = new SelectList(
                    await _districtService.GetDistrictsByCityIdAsync(employeeModel.CityId),
                    "DistrictId",
                    "DistrictName"
                );
                ViewBag.WardList = new SelectList(
                    await _wardService.GetWardsByDistrictIdAsync(employeeModel.DistrictId),
                    "WardId",
                    "WardName"
                );
            }
            else
            {
                ViewBag.DistrictList = new SelectList(new List<District>(), "DistrictId", "DistrictName");
                ViewBag.WardList = new SelectList(new List<Ward>(), "WardId", "WardName");
            }
        }
    }
}
