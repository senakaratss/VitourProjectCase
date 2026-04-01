using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.ReviewServices;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsReviewsComponentPartial:ViewComponent
    {
        private readonly IReviewService _reviewService;

        public _TourDetailsReviewsComponentPartial(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _reviewService.GetAllReviewsByTourIdAsync(id);

            double averageScore = values.Count == 0 ? 0 : values.Average(x => x.Score);

            ViewBag.TourId = id;
            ViewBag.AverageScore = averageScore;
            return View(values);
        }
    }
}
