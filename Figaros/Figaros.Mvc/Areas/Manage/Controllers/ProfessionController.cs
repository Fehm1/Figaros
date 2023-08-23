using Figaros.Entities.DTOs.ProfessionDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _professionService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _professionService.GetAllByDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfessionPostDto professionPostDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _professionService.Add(professionPostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(professionPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _professionService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfessionUpdateDto professionUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _professionService.Update(professionUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(professionUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _professionService.Delete(id);
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
            var result = await _professionService.HardDelete(id);
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
            var result = await _professionService.Restore(id);
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
