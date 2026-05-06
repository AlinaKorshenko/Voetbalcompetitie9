using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels.StadionVanTeamVM;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers
{
    public class StadionController : Controller
    {

        private readonly IService<Team> _setviceTeam;
        private readonly IZitplaatsenService _zitplaatsenService;

        public StadionController(IService<Team> setviceTeam, IZitplaatsenService zitplaatsenService)
        {
            _setviceTeam = setviceTeam;
            _zitplaatsenService = zitplaatsenService;
        }

        public async Task<IActionResult> Index()
        {

            var teams = await _setviceTeam.GetAllAsync();

            var model = new List<StadionVanTeamVM>();

            foreach (var team in teams)
            {
                var zitplaatsen =
                    await _zitplaatsenService.GetByStadionIdAsync(team.StadionId);

                var vm = new StadionVanTeamVM
                {
                    TeamNaam = team.Naam,

                    StadionNaam = team.Stadion.Naam,
                    Land = team.Stadion.Land,
                    Adres = team.Stadion.Adres,
                    Postcode = team.Stadion.Postcode,
                    Gemeente = team.Stadion.Gemeente,

                    AantalZitplaatsen = zitplaatsen.Count(),

                    VakTypes = zitplaatsen
                        .GroupBy(z => new
                        {
                            z.VakNummerNavigation.VakNummer,
                            z.VakNummerNavigation.Ring,
                            z.VakNummerNavigation.Omschrijving
                        })
                        .Select(g => new VakTypeVanStadionVM
                        {
                            VakTypeNaam =
                                "Vak " + g.Key.VakNummer +
                                " | Ring " + g.Key.Ring +
                                " | " + g.Key.Omschrijving,

                            Capaciteit = g.Count()
                        })
                        .ToList()
                };
                model.Add(vm);
            }
            return View(model);
        }
    }
}
