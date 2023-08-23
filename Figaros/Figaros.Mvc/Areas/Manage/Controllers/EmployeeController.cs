using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IProfessionService _professionService;

        public EmployeeController(IEmployeeService employeeService, IProfessionService professionService)
        {
            _employeeService = employeeService;
            _professionService = professionService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _employeeService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _employeeService.GetAllByDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var professions = await _professionService.GetAllByNonDeleted();
            ViewBag.Professions = professions.Data;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeePostDto employeePostDto)
        {
            var professions = await _professionService.GetAllByNonDeleted();
            ViewBag.Professions = professions.Data;

            if (ModelState.IsValid)
            {
                var result = await _employeeService.Add(employeePostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(employeePostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var professions = await _professionService.GetAllByNonDeleted();
            ViewBag.Professions = professions.Data;

            var result = await _employeeService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeUpdateDto employeeUpdateDto)
        {
            var professions = await _professionService.GetAllByNonDeleted();
            ViewBag.Professions = professions.Data;

            if (ModelState.IsValid)
            {
                var result = await _employeeService.Update(employeeUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(employeeUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _employeeService.HardDelete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Restore(int id)
        {
            var result = await _employeeService.Restore(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> DeleteAll()
        {
            var professions = await _professionService.GetAllByDeleted();

            if (professions.ResultStatus == ResultStatus.Success)
            {
                foreach (var profession in professions.Data.Professions)
                {
                    var result = await _professionService.HardDelete(profession.Id);
                    if (result.ResultStatus != ResultStatus.Success)
                    {
                        return NotFound();
                    }
                }

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
