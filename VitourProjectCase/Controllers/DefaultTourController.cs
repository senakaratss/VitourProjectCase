using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class DefaultTourController : Controller
    {
        private readonly ITourService _tourService;

        public DefaultTourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        public IActionResult TourList(int page=1)
        {
            ViewBag.Page = page;
            return View();
        }
        public async Task<IActionResult> TourDetails(string id)
        {
            var value = await _tourService.GetTourDetailAsync(id);
            return View(value);
        }
    }
}
