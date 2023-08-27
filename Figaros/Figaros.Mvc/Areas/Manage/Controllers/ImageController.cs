using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.ImageDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _imageService.GetAllByNonDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _imageService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ImageUpdateDto imageUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _imageService.Update(imageUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(imageUpdateDto);
        }
    }
}
