using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChampionsLeagueTickets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : Controller
    {
        private readonly IMapper _mapper;
        private IMatchService _matchesService;

        public MatchesController(IMapper mapper, IMatchService matchesService)
        {
            _mapper = mapper;
            _matchesService = matchesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<MatchVM>> Get(string homeClubId, string awayClubId)
        {
            try
            {
                var list = await _matchesService.GetAllMatchesFromTeamsAsync(homeClubId, awayClubId);
                List<MatchVM> data = _mapper.Map<List<MatchVM>>(list);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }
    }
}
