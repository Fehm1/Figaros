using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FAQController : Controller
    {
        private readonly IFAQService _FAQService;

        public FAQController(IFAQService FAQService)
        {
            _FAQService = FAQService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _FAQService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _FAQService.GetAllByDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FAQPostDto fAQPostDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _FAQService.Add(fAQPostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(fAQPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _FAQService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FAQUpdateDto fAQUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _FAQService.Update(fAQUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(fAQUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _FAQService.Delete(id);
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
            var result = await _FAQService.HardDelete(id);
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
            var result = await _FAQService.Restore(id);
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
            var FAQs = await _FAQService.GetAllByDeleted();

            if (FAQs.ResultStatus == ResultStatus.Success)
            {
                foreach (var FAQ in FAQs.Data.FAQs)
                {
                    var result = await _FAQService.HardDelete(FAQ.Id);
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
