using Figaros.Entities.DTOs.SponsorDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SponsorController : Controller
    {
        private readonly ISponsorService _sponsorService;

        public SponsorController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _sponsorService.GetAllByNonDeleted();

            return View(result.Data);
        }

        public async Task<IActionResult> DeletedTable()
        {
            var result = await _sponsorService.GetAllByDeleted();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SponsorPostDto sponsorPostDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _sponsorService.Add(sponsorPostDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("index");
                }
            }

            return View(sponsorPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _sponsorService.GetUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SponsorUpdateDto sponsorUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _sponsorService.Update(sponsorUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("index");
                }
            }

            return View(sponsorUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sponsorService.Delete(id);
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
            var result = await _sponsorService.HardDelete(id);
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
            var result = await _sponsorService.Restore(id);
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
            var sponsors = await _sponsorService.GetAllByDeleted();

            if (sponsors.ResultStatus == ResultStatus.Success)
            {
                foreach (var sponsor in sponsors.Data.Sponsors)
                {
                    var result = await _sponsorService.HardDelete(sponsor.Id);
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
