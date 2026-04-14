using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers
{
    public class AbonnementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
