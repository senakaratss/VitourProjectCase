using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsSidebarLast3TourComponentPartial:ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourDetailsSidebarLast3TourComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _tourService.GetLast3TourAsync();
            return View(values);
        }
    }
}
