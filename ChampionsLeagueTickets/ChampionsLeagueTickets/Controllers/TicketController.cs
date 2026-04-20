using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.View_Models;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Voetbalcompetitie9.Controllers
{
    public class TicketController : Controller
    {

        private readonly IMatchService _matchesService;
        private readonly IService<VakType> _vakService;
        private readonly IMapper _mapper;
        private readonly IZitplaatsenService _zitplatsenService;
        private readonly ITicketPrijsService _ticketPrijsService;

        public TicketController(IMatchService matchesService, IMapper mapper, IService<VakType> vakService, IZitplaatsenService zitplaatsenService, ITicketPrijsService ticketPrijsService)
        {
            _matchesService = matchesService;
            _mapper = mapper;
            _vakService = vakService;
            _zitplatsenService = zitplaatsenService;
            _ticketPrijsService = ticketPrijsService;
        }

        public async Task<IActionResult> Matches()
        {
            var list = await _matchesService.GetAllAsync();
            List<MatchVM> matches = _mapper.Map<List<MatchVM>>(list);

            return View(matches);
        }

        public async Task<IActionResult> ChooseSeats(string matchID) {

            var vakTypes = (await _vakService.GetAllAsync())
             .Select(v => new
                {
                  v.VakNummer,
                  DisplayName = "Ring " + v.Ring + " ( " + v.Omschrijving + " )" 
                });

            TicketVM ticketVM = new TicketVM()
            {
                VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName"),
                MatchID = matchID
            }; 


            return View(ticketVM);

        }

        [HttpPost]
        public async Task<IActionResult> ChooseSeats(TicketVM ticketVM)
        {
            var vakTypes = (await _vakService.GetAllAsync())
             .Select(v => new
             {
                 v.VakNummer,
                 DisplayName = "Ring " + v.Ring + " ( " + v.Omschrijving + " )"
             });

            ticketVM.VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName");

            ticketVM.Prijs = await _ticketPrijsService.GetTicketPrijsByMatchAndSectionAsync(ticketVM.MatchID, ticketVM.StadionVak);          

            if (!string.IsNullOrEmpty(ticketVM.StadionVak))
            {
                var rijen = await _zitplatsenService.GetRowsForMatchAndSectionAsync(ticketVM.MatchID, ticketVM.StadionVak);
                ticketVM.RijenLijst = new SelectList(rijen, ticketVM.RijNummer);
            }

            if (!string.IsNullOrEmpty(ticketVM.StadionVak) && !string.IsNullOrEmpty(ticketVM.RijNummer))
            {
                var stoelen = await _zitplatsenService.GetSeatsForMatchSectionAndRowAsync(ticketVM.MatchID, ticketVM.StadionVak, ticketVM.RijNummer);

                var vrijeStoelen = stoelen
                    .Where(s => !s.IsBezet)
                    .Select(s => new
                    {
                        s.ZitplaatsId,
                        DisplayName = "Stoel " + s.StoelNummer
                    });

                ticketVM.StoelenLijst = new SelectList(vrijeStoelen, "ZitplaatsId", "DisplayName", ticketVM.GeselecteerdeZitplaatsId);
            }




            return View(ticketVM);
        }

        public async Task<IActionResult> OverzichtInfoTicket(TicketVM vm)
        {

            var match = await _matchesService.FindByIdAsync(vm.MatchID);
            var thuisTeamNaam = match.ThuisTeam?.Naam;
            var bezoekTeamNaam = match.BezoekendTeam?.Naam;
            var stadionNaam = match.ThuisTeam?.Stadion?.Naam;
            var datum = match.DatumTijdStartMatch;    


            if (match == null)
            {
                return NotFound("Match niet gevonden");
            }

            var zitplaats = await _zitplatsenService.FindByIdAsync(vm.GeselecteerdeZitplaatsId);           

            if (zitplaats == null)
            {
                return NotFound("Zitplaats niet gevonden");
            }

            var zitplaatsInfoVM = new TicketInfoVM
            {
                MatchID = vm.MatchID,
                ThuisTeam = thuisTeamNaam ?? "",
                BezoekTeam = bezoekTeamNaam ?? "",
                Stadion = stadionNaam ?? "",
                Zitplaats = zitplaats,
                Prijs = (decimal)vm.Prijs,
                Datum = datum

            };

            return View(zitplaatsInfoVM);
        }

        public async Task<IActionResult> BevestigTicket(string ZitplaatsID, string MatchID) {
            var cart = HttpContext.Session.GetObject<List<ShoppingCartItemKortVM>>("ShoppingCart")
                       ?? new List<ShoppingCartItemKortVM>();

            var zitplaats = await _zitplatsenService.FindByIdAsync(ZitplaatsID);

            if (zitplaats == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string vakNummer = zitplaats.VakNummer;

            var bestaand = cart.FirstOrDefault(x =>
                x.MatchId == MatchID &&
                x.VakNummer == vakNummer
            );

            if (bestaand != null)
            {
                // проверка лимита (max 4 per match)
                int totaalVoorMatch = cart
                    .Where(x => x.MatchId == MatchID)
                    .Sum(x => x.Aantal);

                if (totaalVoorMatch >= 4)
                {
                    TempData["CartMessage"] = "Je kan maximum 4 tickets per match toevoegen.";
                    return RedirectToAction("Introductie", "Info");
                }

                bestaand.Aantal++;
            }
            else
            {
                // тоже проверка лимита
                int totaalVoorMatch = cart
                    .Where(x => x.MatchId == MatchID)
                    .Sum(x => x.Aantal);

                if (totaalVoorMatch >= 4)
                {
                    TempData["CartMessage"] = "Je kan maximum 4 tickets per match toevoegen.";
                    return RedirectToAction("Introductie", "Info");
                }

                cart.Add(new ShoppingCartItemKortVM
                {
                    MatchId = MatchID,
                    ZitplaatsId = ZitplaatsID,
                    VakNummer = vakNummer,
                    Aantal = 1
                });
            }

            HttpContext.Session.SetObject("ShoppingCart", cart);

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
