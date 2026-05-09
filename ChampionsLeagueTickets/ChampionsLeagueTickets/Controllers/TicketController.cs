using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ChampionsLeagueTickets.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using ChampionsLeagueTickets.ViewModels.Zitplaatsen;

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

        public async Task<IActionResult> Matches(string club)
        {
            var list = await _matchesService.GetAllAsync();

            if (!string.IsNullOrEmpty(club))
            {
                list = list.Where(m =>
                    m.ThuisTeam.Naam == club ||
                    m.BezoekendTeam.Naam == club
                ).ToList();
            }

            var matches = _mapper.Map<List<MatchVM>>(list);

            foreach (var match in matches)
            {
                match.IsKoopbaar = IsTicketBeschikbaar(match.DatumTijdStartMatch);
            }

            ViewBag.SelectedClub = club;

            return View(matches);
        }

        [Authorize]
        public async Task<IActionResult> ChooseSeats(string matchID)
        {
            var match = await _matchesService.FindByIdAsync(matchID);

            if (match == null)
            {
                return NotFound();
            }

            if (!IsTicketBeschikbaar(match.DatumTijdStartMatch))
            {
                TempData["Error"] = "Tickets voor deze match zijn nog niet beschikbaar.";
                return RedirectToAction("Matches");
            }

            var vakTypes = (await _vakService.GetAllAsync())
                .Select(v => new
                {
                    v.VakNummer,
                    DisplayName = v.Omschrijving
                });

            TicketStoelVM ticketVM = new TicketStoelVM()
            {
                VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName"),
                MatchID = matchID
            };

            return View(ticketVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChooseSeats(TicketStoelVM ticketVM)
        {
            var match = await _matchesService.FindByIdAsync(ticketVM.MatchID);

            if (match == null)
                return NotFound();

            if (!IsTicketBeschikbaar(match.DatumTijdStartMatch))
            {
                TempData["Error"] = "Tickets voor deze match zijn nog niet beschikbaar.";
                return RedirectToAction("Matches");
            }

            if (!ModelState.IsValid)
            {
                return View(ticketVM);
            }

            var vakTypes = (await _vakService.GetAllAsync())
             .Select(v => new
             {
                 v.VakNummer,
                 DisplayName = v.Omschrijving
             });

            ticketVM.VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName");

            ticketVM.Prijs = await _ticketPrijsService.GetTicketPrijsByMatchAndSectionAsync(ticketVM.MatchID, ticketVM.StadionVak);

            var stadion = await _matchesService.GetStadionByMatchIdAsync(ticketVM.MatchID);

            if (stadion == null)
            {
                return NotFound();
            }

            var stadionID = stadion.StadionId;

            if (!string.IsNullOrEmpty(ticketVM.StadionVak))
            {
                var rijen = await _zitplatsenService.GetRowsForSectionAsync(stadionID, ticketVM.StadionVak);
                ticketVM.RijenLijst = new SelectList(rijen, ticketVM.RijNummer);
            }

            if (!string.IsNullOrEmpty(ticketVM.StadionVak) && !string.IsNullOrEmpty(ticketVM.RijNummer))
            {
                var stoelen = await _zitplatsenService.GetSeatsForMatchSectionAndRowAsync(
                   ticketVM.MatchID,
                   ticketVM.StadionVak,
                   ticketVM.RijNummer
               );

                var ticketCart = HttpContext.Session.GetObject<List<ShoppingCartTicketItemKortVM>>("ShoppingCartTicket")
                                 ?? new List<ShoppingCartTicketItemKortVM>();
                var abonnementCart = HttpContext.Session.GetObject<List<ShoppingCartTicketItemKortVM>>("ShoppingCartAbonement")
                     ?? new List<ShoppingCartTicketItemKortVM>();

                var vrijeStoelen = stoelen
                     .Where(s =>
            !s.IsBezet &&

            !ticketCart.Any(t =>
                t.MatchId == ticketVM.MatchID &&
                t.ZitplaatsId == s.ZitplaatsId
            ) &&

            !abonnementCart.Any(a =>
                a.ZitplaatsId == s.ZitplaatsId
            )
        )
                    .Select(s => new
                    {
                        s.ZitplaatsId,
                        DisplayName = "Stoel " + s.StoelNummer
                    });

                ticketVM.StoelenLijst = new SelectList(
                    vrijeStoelen,
                    "ZitplaatsId",
                    "DisplayName"
                );
            }

            return View(ticketVM);
        }

        [Authorize]
        public async Task<IActionResult> OverzichtInfoTicket(TicketStoelVM vm)
        {
            var match = await _matchesService.FindByIdAsync(vm.MatchID);

            if (match == null)
            {
                return NotFound("Match niet gevonden");
            }

            if (!IsTicketBeschikbaar(match.DatumTijdStartMatch))
            {
                TempData["Error"] = "Tickets voor deze match zijn nog niet beschikbaar.";
                return RedirectToAction("Matches");
            }

            var thuisTeamNaam = match.ThuisTeam?.Naam;
            var bezoekTeamNaam = match.BezoekendTeam?.Naam;
            var stadionNaam = match.ThuisTeam?.Stadion?.Naam;
            var datum = match.DatumTijdStartMatch;

            var zitplaats = await _zitplatsenService.FindByIdAsync(vm.GeselecteerdeZitplaatsId);

            if (zitplaats == null)
            {
                return NotFound("Zitplaats niet gevonden");
            }

            var zitplaatsVM = _mapper.Map<ZitplaatsVM>(zitplaats);

            var zitplaatsInfoVM = new TicketInfoVM
            {
                MatchID = vm.MatchID,
                ThuisTeam = thuisTeamNaam ?? "",
                BezoekTeam = bezoekTeamNaam ?? "",
                Stadion = stadionNaam ?? "",
                Zitplaats = zitplaatsVM,
                Prijs = (decimal)vm.Prijs,
                Datum = datum
            };

            return View(zitplaatsInfoVM);
        }

        [Authorize]
        public async Task<IActionResult> BevestigTicket(string ZitplaatsID, string MatchID)
        {
            if (string.IsNullOrEmpty(ZitplaatsID) || string.IsNullOrEmpty(MatchID))
                return BadRequest();

            var match = await _matchesService.FindByIdAsync(MatchID);

            if (match == null)
            {
                return NotFound();
            }

            if (!IsTicketBeschikbaar(match.DatumTijdStartMatch))
            {
                TempData["Error"] = "Tickets voor deze match zijn nog niet beschikbaar.";
                return RedirectToAction("Matches");
            }

            var cart = HttpContext.Session.GetObject<List<ShoppingCartTicketItemKortVM>>("ShoppingCartTicket")
                       ?? new List<ShoppingCartTicketItemKortVM>();

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
                int totaalVoorMatch = cart
                    .Where(x => x.MatchId == MatchID)
                    .Sum(x => x.Aantal);

                if (totaalVoorMatch >= 4)
                {
                    TempData["CartMessage"] = "Je kan maximum 4 tickets per match toevoegen.";
                    return RedirectToAction("Introductie", "Info");
                }

                cart.Add(new ShoppingCartTicketItemKortVM
                {
                    MatchId = MatchID,
                    ZitplaatsId = ZitplaatsID,
                    VakNummer = vakNummer,
                    Aantal = 1
                });
            }

            HttpContext.Session.SetObject("ShoppingCartTicket", cart);

            return RedirectToAction("Index", "ShoppingCart");
        }

        private bool IsTicketBeschikbaar(DateTime matchDatum)
        {
            var now = DateTime.Now;
            var oneMonthBeforeMatch = matchDatum.AddMonths(-1);

            return now >= oneMonthBeforeMatch && now < matchDatum;
        }
    }
}
