using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace VitourProjectCase.Controllers
{
    public class LanguageController : Controller
    {
       
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            // Cookie oluşturup kullanıcı dilini ayarla
           
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            

            return LocalRedirect(returnUrl ?? "/");
        }
       
    }
}
