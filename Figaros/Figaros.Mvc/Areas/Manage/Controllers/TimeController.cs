using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TimeController : Controller
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _timeService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> Deactive(int id)
        {
            var result = await _timeService.Deactive(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> Active(int id)
        {
            var result = await _timeService.Active(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
