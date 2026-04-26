using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
