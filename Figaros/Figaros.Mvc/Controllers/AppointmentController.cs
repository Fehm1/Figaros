using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IBookingService _bookingService;

        public AppointmentController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingPostDto bookingPostDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookingService.Add(bookingPostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return View("Success");
                }
            }

            return RedirectToAction("index", "home", new { area = "" });
        }
    }
}
