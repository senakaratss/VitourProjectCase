using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsInformationComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourDetailsInformationComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _tourService.GetTourDetailAsync(id);
            return View(value);
        }
    }
}
