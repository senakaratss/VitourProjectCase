using Microsoft.AspNetCore.Mvc;
using VitourProjectCase.Dtos.ReservationDtos;
using VitourProjectCase.Exceptions.ReservationExceptions;
using VitourProjectCase.Services.ReservationServices;
using VitourProjectCase.Services.TourServices;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace VitourProjectCase.Controllers
{
    public class DefaultReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;

        public DefaultReservationController(IReservationService reservationService, ITourService tourService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
        }

        public async Task<IActionResult> CreateReservation()
        {
            ViewBag.Tours = await _tourService.GetAllTourAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            try
            {
                var code = await _reservationService.CreateReservationAsync(createReservationDto);

                return RedirectToAction("ReservationConfirm", new { code });
            }
            catch (CapacityExceededException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                ViewBag.Tours = await _tourService.GetAllTourAsync();
                return View(createReservationDto);
            }

        }
        public async Task<IActionResult> ReservationConfirm(string code)
        {
            var reservation = await _reservationService.GetReservationByCodeAsync(code);
            return View(reservation);
        }
    }
}
