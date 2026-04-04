using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
