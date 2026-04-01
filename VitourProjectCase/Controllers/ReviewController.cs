using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitourProjectCase.Dtos.ReviewDtos;
using VitourProjectCase.Services.ReviewServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITourService _tourService;

        public ReviewController(IReviewService reviewService, ITourService tourService)
        {
            _reviewService = reviewService;
            _tourService = tourService;
        }

        public async Task<IActionResult> ReviewList()
        {
            var values = await _reviewService.GetAllReviewAsync();
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.TourId
            }).ToList();
            return View(values);
        }
        public async Task<IActionResult> ReviewListByTourId(string id)
        {
            ViewBag.Tour = await _tourService.GetTourByIdAsync(id);
            var values = await _reviewService.GetAllReviewsByTourIdAsync(id);
            return View(values);
        }
        public IActionResult CreateReview()
        {           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            createReviewDto.Status = true;
            createReviewDto.ReviewDate = DateTime.UtcNow;
            await _reviewService.CreateReviewAsync(createReviewDto);
            return RedirectToAction("TourDetails", "DefaultTour", new { id=createReviewDto.TourId});
        }
    }
}
