using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PriceController : Controller
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _priceService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _priceService.GetAllByDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PricePostDto pricePostDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _priceService.Add(pricePostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(pricePostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _priceService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PriceUpdateDto priceUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _priceService.Update(priceUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(priceUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _priceService.Delete(id);
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
            var result = await _priceService.HardDelete(id);
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
            var result = await _priceService.Restore(id);
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
            var prices = await _priceService.GetAllByDeleted();

            if (prices.ResultStatus == ResultStatus.Success)
            {
                foreach (var price in prices.Data.Prices)
                {
                    var result = await _priceService.HardDelete(price.Id);
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
