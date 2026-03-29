using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    public class APIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
