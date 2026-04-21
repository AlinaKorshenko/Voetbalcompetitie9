using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartKey = "ShoppingCart";
        private readonly IMatchService _matchesService;
        private readonly IService<VakType> _vakService;
        private readonly IZitplaatsenService _zitplatsenService;
        private readonly ITicketPrijsService _ticketPrijsService;

        public ShoppingCartController(IMatchService matchesService, IService<VakType> vakService, IZitplaatsenService zitplatsenService, ITicketPrijsService ticketPrijsService)
        {
            _matchesService = matchesService;
            _vakService = vakService;
            _zitplatsenService = zitplatsenService;
            _ticketPrijsService = ticketPrijsService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var carts =  await GetShoppingCart();
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
            var cartTickets = HttpContext.Session.GetObject<List<ShoppingCartItemKortVM>>(CartKey);
            var result = new ShoppingCartVM();
            var tickets = new List<ShoppingCartTicketVM>();

            if (cartTickets == null || !cartTickets.Any())
                return result;

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
            return result;
        }

        private void SaveShoppingCart(ShoppingCartVM cart)
        {
            HttpContext.Session.SetObject(CartKey, cart);
        }

    }
}
