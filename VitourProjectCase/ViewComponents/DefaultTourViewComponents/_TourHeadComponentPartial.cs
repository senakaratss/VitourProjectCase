using Microsoft.AspNetCore.Mvc;

namespace VitourProjectCase.ViewComponents.DefaultTourViewComponents
{
    public class _TourHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
