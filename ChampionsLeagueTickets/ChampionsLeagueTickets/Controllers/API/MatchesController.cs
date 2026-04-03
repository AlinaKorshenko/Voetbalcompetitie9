using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : Controller
    {
        private readonly IMapper _mapper;
        private IService<Match> _matchesService;

        public MatchesController(IMapper mapper, IService<Match> matchesService)
        {
            _mapper = mapper;
            _matchesService = matchesService;
        }
    }
}
