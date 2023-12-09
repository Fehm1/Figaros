using Figaros.Entities.DTOs.SettingDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _settingService.Get(2);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _settingService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(SettingUpdateDto settingUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.Update(settingUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(settingUpdateDto);
        }
    }
}
