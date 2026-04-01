using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.TourPlanServices;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsPlanComponentPartial:ViewComponent
    {
        private readonly ITourPlanService _tourPlanService;

        public _TourDetailsPlanComponentPartial(ITourPlanService tourPlanService)
        {
            _tourPlanService = tourPlanService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _tourPlanService.GetAllTourPlanByTourIdAsync(id);
            return View(values);
        }
    }
}
