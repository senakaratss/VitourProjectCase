using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Dtos.TourPlanDtos;
using VitourProjectCase.Services.TourPlanServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class TourPlanController : Controller
    {
        private readonly ITourPlanService _tourPlanService;
        private readonly ITourService _tourService;

        public TourPlanController(ITourPlanService tourPlanService, ITourService tourService)
        {
            _tourPlanService = tourPlanService;
            _tourService = tourService;
        }

        public async Task<IActionResult> TourPlanList(string id)
        {
            var values = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);
            var tour = await _tourService.GetTourByIdAsync(id);
            ViewBag.TourName = tour.Title;
            ViewBag.TourId = tour.TourId;
            ViewBag.TourDayCount = tour.DayCount;
            ViewBag.TourPrice = tour.Price;
            ViewBag.TourCoverImage = tour.CoverImageUrl;
            return View(values);
        }
        public async Task<IActionResult> AllTourPlan()
        {
            var values = await _tourPlanService.GetAllTourPlanAsync();
            ViewBag.Tours = await _tourService.GetAllTourAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTourPlan(string id)
        {
            ViewBag.TourId = id;
            ViewBag.ExistingPlans = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTourPlan(CreateTourPlanDto createTourPlanDto)
        {
            await _tourPlanService.CreateTourPlanAsync(createTourPlanDto);
            return RedirectToAction("TourPlanList", new { id = createTourPlanDto.TourId });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTourPlan(string id)
        {
            var value = await _tourPlanService.GetTourPlanByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTourPlan(UpdateTourPlanDto updateTourPlanDto)
        {
            await _tourPlanService.UpdateTourPlanAsync(updateTourPlanDto);
            return RedirectToAction("TourPlanList", new { id = updateTourPlanDto.TourId });
        }
        public async Task<IActionResult> DeleteTourPlan(string id, string tourId)
        {
            await _tourPlanService.DeleteTourPlanAsync(id);
            return RedirectToAction("TourPlanList", new { id = tourId });
        }
    }
}
