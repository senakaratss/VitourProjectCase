using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using VitourProjectCase.Dtos.ReservationDtos;
using VitourProjectCase.Services.ReservationServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;

        public ReservationController(IReservationService reservationService, ITourService tourService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
        }

        public async Task<IActionResult> ReservationList()
        {
            var values = await _reservationService.GetAllReservationAsync();
            ViewBag.Tours = await _tourService.GetAllTourAsync();
            return View(values);
        }
        public async Task<IActionResult> ReservationListByTourId(string tourId)
        {
            var values = await _reservationService.GetReservationsByTourIdAsync(tourId);
            return View(values);
        }
        public async Task<IActionResult> CreateReservation()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            await _reservationService.CreateReservationAsync(createReservationDto);
            return RedirectToAction("ReservationList");
        }
        public async Task<IActionResult> DeleteReservation(string id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("ReservationList");
        }
    }
}
