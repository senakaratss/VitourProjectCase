using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using VitourProjectCase.Dtos.GalleryDtos;
using VitourProjectCase.Services.GalleryServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly ITourService _tourService;

        public GalleryController(IGalleryService galleryService, ITourService tourService)
        {
            _galleryService = galleryService;
            _tourService = tourService;
        }

        public async Task<IActionResult> GalleryList()
        {
            var values = await _galleryService.GetAllImagesAsync();
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours;
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateImage()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = tours.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.TourId
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateImage(CreateGalleryDto createGalleryDto)
        {
            await _galleryService.CreateImageAsync(createGalleryDto);
            return RedirectToAction("GalleryList");
        }
        public async Task<IActionResult> DeleteImage(string id)
        {
            await _galleryService.DeleteImageAsync(id);
            return RedirectToAction("GalleryList");
        }
    }
}
