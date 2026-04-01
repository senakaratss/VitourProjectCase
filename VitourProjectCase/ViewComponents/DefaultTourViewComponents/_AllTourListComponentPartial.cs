using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.ViewComponents.DefaultTourViewComponents
{
    public class _AllTourListComponentPartial:ViewComponent
    {
        private readonly ITourService _tourService;

        public _AllTourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page)
        {
            int pageSize = 6;
            var (tours,totalCount) = await _tourService.GetPagedToursAsync(page,pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(tours);
        }
    }
}
