using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly ISettingService _settingService;

        public RequestController(IRequestService requestService, ISettingService settingService)
        {
            _requestService = requestService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _requestService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _requestService.GetAllByDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _requestService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Info(int id)
        {
            var result = await _requestService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Readed(int id)
        {
            var result = await _requestService.Readed(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _requestService.HardDelete(id);
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
            var result = await _requestService.Restore(id);
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
            var requests = await _requestService.GetAllByDeleted();

            if (requests.ResultStatus == ResultStatus.Success)
            {
                foreach (var request in requests.Data.Requests)
                {
                    var result = await _requestService.HardDelete(request.Id);
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
