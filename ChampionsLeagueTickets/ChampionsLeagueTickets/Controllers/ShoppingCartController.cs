using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartKeyTicket = "ShoppingCartTicket";
        private const string CartKeyAbonement = "ShoppingCartAbonement";
        private readonly IMatchService _matchesService;
        private readonly IService<VakType> _vakService;
        private readonly IZitplaatsenService _zitplatsenService;
        private readonly ITicketPrijsService _ticketPrijsService;
        private readonly IService<Stadion> _stadionService;
        private readonly IAbonementenPrijsService _abonementenPrijsService;
        private readonly ISeizoenenService _seizoenenService;
        public ShoppingCartController(IMatchService matchesService, IService<VakType> vakService, IZitplaatsenService zitplatsenService, ITicketPrijsService ticketPrijsService, IService<Stadion> stadionService, IAbonementenPrijsService abonementenPrijsService, ISeizoenenService seizoenenService)
        {
            _matchesService = matchesService;
            _vakService = vakService;
            _zitplatsenService = zitplatsenService;
            _ticketPrijsService = ticketPrijsService;
            _stadionService = stadionService;
            _abonementenPrijsService = abonementenPrijsService;
            _seizoenenService = seizoenenService;

        }

        public async Task<IActionResult> IndexAsync()
        {
            var carts = await GetShoppingCart();
            return View(carts);
        }

        public IActionResult AddToCart(ShoppingCartVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            return RedirectToAction("Index");
        }
        private async Task<ShoppingCartVM> GetShoppingCart()
        {
            var cartTickets = HttpContext.Session.GetObject<List<ShoppingCartTicketItemKortVM>>(CartKeyTicket);
            var result = new ShoppingCartVM();
            var tickets = new List<ShoppingCartTicketVM>();

            if (cartTickets != null && cartTickets.Any())
            {


                foreach (var item in cartTickets)
                {
                    var match = await _matchesService.FindByIdAsync(item.MatchId);
                    var zitplaats = await _zitplatsenService.FindByIdAsync(item.ZitplaatsId);
                    if (zitplaats == null || match == null || zitplaats.VakNummerNavigation == null)
                    {
                        continue;
                    }

                    var prijs = await _ticketPrijsService.GetTicketPrijsByMatchAndSectionAsync(item.MatchId, zitplaats.VakNummer);
                    var vak = zitplaats.VakNummerNavigation.Omschrijving + " (Ring " + zitplaats.VakNummerNavigation.Ring + ")";


                    if (zitplaats != null && match != null)
                    {
                        result.Tickets.Add(new ShoppingCartTicketVM
                        {
                            MatchId = match.MatchId,
                            ZitplaatsId = zitplaats.ZitplaatsId,
                            ThuisTeam = match.ThuisTeam.Naam,
                            UitTeam = match.BezoekendTeam.Naam,
                            MatchDateTime = match.DatumTijdStartMatch,
                            Stadion = match.ThuisTeam.Stadion.Naam,
                            Vak = vak,
                            Rij = zitplaats.RijNummer,
                            Stoel = zitplaats.StoelNummer,
                            Prijs = prijs,
                            Aantal = item.Aantal
                        });
                    }
                }

            }
            var cartAbonementen = HttpContext.Session.GetObject<List<ShoppingCartAbonementItemKortVM>>(CartKeyAbonement);

            if (cartAbonementen == null || !cartAbonementen.Any())
            {
                return result;
            }



            foreach (var item in cartAbonementen)
            {


                var stadion = await _stadionService.FindByIdAsync(item.StadionId);
                var seizoen = await _seizoenenService.FindByIdAsync(item.SeizoenId);
                var zitplaats = await _zitplatsenService.FindByIdAsync(item.ZitplaatsId);

                    var prijs = await _abonementenPrijsService.GetPriceBySeizoenIdAndStadionId(item.SeizoenId, item.StadionId);

                    result.Abonementen.Add(new AbonementOverzichtVM
                    {

                        SeizoenId = item.SeizoenId,
                        StadionID = item.StadionId,
                        StadionNaam = stadion.Naam,
                        zitplaats = zitplaats,
                        SeizoenNaam = seizoen.Naam,
                        StartDatum = seizoen.StartDatum,
                        EindDatum = seizoen.EindDatum,
                        Prijs = prijs
                    });
                }


            return result;
        }


        [HttpPost]
        public IActionResult VerwijderTicket(string matchId, string zitplaatsId)
        {
            var cartTickets = HttpContext.Session
                .GetObject<List<ShoppingCartTicketItemKortVM>>(CartKeyTicket);

            if (cartTickets == null || !cartTickets.Any())
            {
                return RedirectToAction("Index");
            }

            var item = cartTickets.FirstOrDefault(x =>
                x.MatchId == matchId && x.ZitplaatsId == zitplaatsId);

            if (item != null)
            {
                if (item.Aantal > 1)
                {
                    item.Aantal -= 1; 
                }
                else
                {
                    cartTickets.Remove(item); 
                }
            }

            HttpContext.Session.SetObject(CartKeyTicket, cartTickets);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VoegTicketToe(string matchId, string zitplaatsId)
        {
            var cartTickets = HttpContext.Session
                .GetObject<List<ShoppingCartTicketItemKortVM>>(CartKeyTicket)
                ?? new List<ShoppingCartTicketItemKortVM>();

            var item = cartTickets.FirstOrDefault(x =>
                x.MatchId == matchId && x.ZitplaatsId == zitplaatsId);

            if (item != null)
            {
                if (item.Aantal >= 4)
                {
                    TempData["Error"] = "Je kan maximaal 4 tickets per plaats toevoegen.";
                    return RedirectToAction("Index");
                }

                item.Aantal += 1;
            }
           

            HttpContext.Session.SetObject(CartKeyTicket, cartTickets);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult VerwijderAbonnement(string stadionId, string seizoenId, string zitplaatsId)
        {
            var cartAbonementen = HttpContext.Session
                .GetObject<List<ShoppingCartAbonementItemKortVM>>(CartKeyAbonement);

            if (cartAbonementen == null)
            {
                return RedirectToAction("Index");
            }

            var item = cartAbonementen.FirstOrDefault(x =>
                x.StadionId == stadionId &&
                x.SeizoenId == seizoenId &&
                x.ZitplaatsId == zitplaatsId);

            if (item == null)
            {
                TempData["Error"] = $"Niet gevonden: stadion={stadionId}, seizoen={seizoenId}, zitplaats={zitplaatsId}";
                return RedirectToAction("Index");
            }

            cartAbonementen.Remove(item);
            HttpContext.Session.SetObject(CartKeyAbonement, cartAbonementen);

            return RedirectToAction("Index");
        }



    }
    }

