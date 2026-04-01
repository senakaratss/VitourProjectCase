using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitourProjectCase.Dtos.TourDtos;
using VitourProjectCase.Services.CategoryServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;

        public TourController(ITourService tourService, ICategoryService categoryService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> TourList()
        {
            var tours = await _tourService.GetAllTourWithCategoryAsync();
            var categories = await _categoryService.GetAllCategoryAsync();
           
            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Value = x.CategoryId,
                Text = x.CategoryName
            }).ToList();
            return View(tours);
        }  
        public async Task<IActionResult> CreateTour()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = new SelectList(values,"CategoryId","CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            createTourDto.CreatedDate = DateTime.UtcNow;
            await _tourService.CreateTourAsync(createTourDto);
            return RedirectToAction("TourList");
        }
        public async Task<IActionResult> DeleteTour(string id)
        {
            await _tourService.DeleteTourAsync(id);
            return RedirectToAction("TourList");
        }
        public async Task<IActionResult> UpdateTour(string id)
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            var value = await _tourService.GetTourByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
        {
            updateTourDto.CreatedDate = DateTime.UtcNow;
            await _tourService.UpdateTourAsync(updateTourDto);
            return RedirectToAction("TourList");
        }
    }
}
