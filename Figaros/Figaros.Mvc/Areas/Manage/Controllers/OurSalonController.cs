using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OurSalonController : Controller
    {
        private readonly IOurSalonService _ourSalonService;

        public OurSalonController(IOurSalonService ourSalonService)
        {
            _ourSalonService = ourSalonService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _ourSalonService.GetAll();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _ourSalonService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OurSalonUpdateDto ourSalonUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ourSalonService.Update(ourSalonUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(ourSalonUpdateDto);
        }
    }
}
