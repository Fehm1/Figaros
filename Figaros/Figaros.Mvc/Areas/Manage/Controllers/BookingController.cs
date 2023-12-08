using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IPriceService _priceService;
        private readonly IEmployeeService _employeeService;
        private readonly ITimeService _timeService;

        public BookingController(IBookingService bookingService, IPriceService priceService, IEmployeeService employeeService, ITimeService timeService)
        {
            _bookingService = bookingService;
            _priceService = priceService;
            _employeeService = employeeService;
            _timeService = timeService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookingService.GetAllByNonDeleted();
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var prices = await _priceService.GetAllByNonDeleted();
            var employess = await _employeeService.GetAllByNonDeleted();
            var times = await _timeService.GetAllByNonDeleted();
            ViewBag.Prices = prices.Data.Prices;
            ViewBag.Employees = employess.Data.Employees;
            ViewBag.Times = times.Data.Times;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookingPostDto bookingPostDto)
        {
            var prices = await _priceService.GetAllByNonDeleted();
            var employess = await _employeeService.GetAllByNonDeleted();
            var times = await _timeService.GetAllByNonDeleted();
            ViewBag.Prices = prices.Data.Prices;
            ViewBag.Employees = employess.Data.Employees;
            ViewBag.Times = times.Data.Times;

            if (ModelState.IsValid)
            {
                var result = await _bookingService.Add(bookingPostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(bookingPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var prices = await _priceService.GetAllByNonDeleted();
            var employess = await _employeeService.GetAllByNonDeleted();
            var times = await _timeService.GetAllByNonDeleted();
            ViewBag.Prices = prices.Data.Prices;
            ViewBag.Employees = employess.Data.Employees;
            ViewBag.Times = times.Data.Times;

            var result = await _bookingService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookingUpdateDto bookingUpdateDto)
        {
            var prices = await _priceService.GetAllByNonDeleted();
            var employess = await _employeeService.GetAllByNonDeleted();
            var times = await _timeService.GetAllByNonDeleted();
            ViewBag.Prices = prices.Data.Prices;
            ViewBag.Employees = employess.Data.Employees;
            ViewBag.Times = times.Data.Times;

            if (ModelState.IsValid)
            {
                var result = await _bookingService.Update(bookingUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }
            return View(bookingUpdateDto);
        }

        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _bookingService.HardDelete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Completed(int id)
        {
            var result = await _bookingService.Completed(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction("index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
