using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _aboutService.Get(1);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _aboutService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AboutUpdateDto aboutUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _aboutService.Update(aboutUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(aboutUpdateDto);
        }
    }
}
