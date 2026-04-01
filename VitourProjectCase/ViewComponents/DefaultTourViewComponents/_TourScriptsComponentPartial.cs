using Microsoft.AspNetCore.Mvc;

namespace VitourProjectCase.ViewComponents.DefaultTourViewComponents
{
    public class _TourScriptsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
