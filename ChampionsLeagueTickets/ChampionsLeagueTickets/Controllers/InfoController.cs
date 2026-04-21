using Microsoft.AspNetCore.Mvc;

namespace Voetbalcompetitie9.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Introductie()
        {
            return View();
        }
        
        public IActionResult OverOns()
        {
            return View();
        }
    }
}
