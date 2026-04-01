using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Dtos.CategoryDtos;
using VitourProjectCase.Services.CategoryServices;

namespace VitourProjectCase.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateCategory()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            ViewBag.categories = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("CategoryList");
        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var value = await _categoryService.GetCategoryByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete([FromBody] List<string> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest();
            await _categoryService.BulkDeleteAsync(ids);
            return Ok(new { success = true });
        }
    }
}
