using Microsoft.AspNetCore.Mvc;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsRightSidebarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
