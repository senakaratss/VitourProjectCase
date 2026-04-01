using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.GalleryServices;

namespace VitourProjectCase.ViewComponents.DefaultTourDetailsViewComponents
{
    public class _TourDetailsGalleryComponentPartial:ViewComponent
    {
        private readonly IGalleryService _galleryService;

        public _TourDetailsGalleryComponentPartial(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _galleryService.GetAllImagesByTourIdAsync(id);
            return View(values);
        }
    }
}
