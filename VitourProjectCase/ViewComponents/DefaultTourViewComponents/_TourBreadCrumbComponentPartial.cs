using Microsoft.AspNetCore.Mvc;

namespace VitourProjectCase.ViewComponents.DefaultTourViewComponents
{
    public class _TourBreadCrumbComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
