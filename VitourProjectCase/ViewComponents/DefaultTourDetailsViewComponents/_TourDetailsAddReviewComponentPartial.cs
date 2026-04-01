using Microsoft.AspNetCore.Mvc;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsAddReviewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke(string id)   
        {
            ViewBag.TourId = id;
            return View();
        }
    }
}
