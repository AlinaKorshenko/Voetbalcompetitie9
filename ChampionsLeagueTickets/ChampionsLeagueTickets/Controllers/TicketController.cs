using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voetbalcompetitie9.ViewModels;

namespace Voetbalcompetitie9.Controllers
{
    public class TicketController : Controller
    {

        private readonly IService<Match> _matchesService;
        private readonly IMapper _mapper;

        public TicketController(IService<Match> matchesService, IMapper mapper)
        {
            _matchesService = matchesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Matches()
        {
            var list = await _matchesService.GetAllAsync();
            List<MatchVM> matches = _mapper.Map<List<MatchVM>>(list);

            return View(matches);
        }

        public async Task<IActionResult> Tickets() {



            return View();

        }


    }
}
