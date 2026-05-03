using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.View_Models;
using ChampionsLeagueTickets.ViewModels;
using ChampionsLeagueTickets.ViewModels.order;
using ChampionsLeagueTickets.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeagueTickets.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAbonementenPrijsService _abonnementenPrijsService;
        private readonly ISeizoenenService _seizoenenService;
        private readonly IZitplaatsenService _zitplaatsenService;
        private readonly IService<VakType> _vakService;
        private readonly IService<Stadion> _stadionService;

        public AbonnementController(IMapper mapper, IAbonementenPrijsService abonnementenPrijsService, ISeizoenenService seizoenenService, IZitplaatsenService zitplaatsenService, IService<VakType> vakCervice, IService<Stadion> stadionService)
        {
            _mapper = mapper;
            _abonnementenPrijsService = abonnementenPrijsService;
            _seizoenenService = seizoenenService;
            _zitplaatsenService = zitplaatsenService;
            _vakService = vakCervice;
            _stadionService = stadionService;
        }

        public async Task<IActionResult> Index()
        {
            var abonnementenPrijzen = await _abonnementenPrijsService.GetAllAsync();
            var vm = _mapper.Map<IEnumerable<AbonnementenInformatieVM>>(abonnementenPrijzen);

            foreach (var abonnement in vm)
            {
                abonnement.IsKoopbaar = IsAbonnementBeschikbaar(abonnement.StartDatum);
            }

            return View(vm);
        }

        private bool IsAbonnementBeschikbaar(DateOnly startDatum)
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Now);

            return vandaag < startDatum;
        }

        [Authorize]
        public async Task<IActionResult> ChooseSeats(AbonnementenInformatieVM abonement)
        {
            var vakTypes = (await _vakService.GetAllAsync())
             .Select(v => new
             {
                 v.VakNummer,
                 DisplayName = "Ring " + v.Ring + " ( " + v.Omschrijving + " )"
             });

            AbonementStoelVM abonementStoelVM = new AbonementStoelVM()
            {
                VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName"),
                SeizoenId = abonement.SeizoenId,
                StadionID = abonement.StadionId
                
            };

            return View(abonementStoelVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChooseSeats(AbonementStoelVM abonementStoelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(abonementStoelVM);
            }

            var vakTypes = (await _vakService.GetAllAsync())
             .Select(v => new
             {
                 v.VakNummer,
                 DisplayName = "Ring " + v.Ring + " ( " + v.Omschrijving + " )"
             });

            abonementStoelVM.VakenLijst = new SelectList(vakTypes, "VakNummer", "DisplayName");


            if (!string.IsNullOrEmpty(abonementStoelVM.StadionVak))
            {
                var rijen = await _zitplaatsenService.GetRowsForSectionAsync(abonementStoelVM.StadionID, abonementStoelVM.StadionVak);
                abonementStoelVM.RijenLijst = new SelectList(rijen, abonementStoelVM.RijNummer);
            }

            if (!string.IsNullOrEmpty(abonementStoelVM.StadionVak) && !string.IsNullOrEmpty(abonementStoelVM.RijNummer))
            {
                var vrijeStoelen = await _zitplaatsenService.GetFreeSeatsForSeasonSectionAndRowAsync(abonementStoelVM.StadionID, abonementStoelVM.SeizoenId, abonementStoelVM.StadionVak, abonementStoelVM.RijNummer);

                var stoelenSelectList = vrijeStoelen
                    .Select(s => new
                    {
                        ZitplaatsId = s.ZitplaatsId,
                        DisplayName = "Stoel " + s.StoelNummer
                    })
                    .ToList();

                abonementStoelVM.StoelenLijst = new SelectList(
                    stoelenSelectList,
                    "ZitplaatsId",
                    "DisplayName",
                    abonementStoelVM.GeselecteerdeZitplaatsId
                    );
            }

            return View(abonementStoelVM);
        }

        [Authorize]
        public async Task<IActionResult> OverzichtInfoAbonement(AbonementStoelVM stoelVm) {

            var stadion = await _stadionService.FindByIdAsync(stoelVm.StadionID);
            var zitplaats = await _zitplaatsenService.FindByIdAsync(stoelVm.GeselecteerdeZitplaatsId);
            var seizoen = await _seizoenenService.FindByIdAsync(stoelVm.SeizoenId);

            var zitplaatsVM = _mapper.Map<ZitplaatsVM>(zitplaats);

            if (zitplaats == null)
            {
                return NotFound("Zitplaats niet gevonden");
            }


            var zitplaatsInfoVM = new AbonementOverzichtVM
            {

                SeizoenId = stoelVm.SeizoenId,
                StadionID = stoelVm.StadionID,
                StadionNaam = stadion.Naam,
                zitplaats = zitplaatsVM,
                SeizoenNaam = seizoen.Naam,
                StartDatum = seizoen.StartDatum,
                EindDatum = seizoen.EindDatum,
                Prijs = await _abonnementenPrijsService.GetPriceBySeizoenIdAndStadionId(stoelVm.SeizoenId, stoelVm.StadionID)


            };

            return View(zitplaatsInfoVM);
        }

        [Authorize]
        public async Task<IActionResult> Bevestig(string SeizoenId, string StadionID, string ZitplaatsId)
        {
            if (string.IsNullOrEmpty(SeizoenId) || string.IsNullOrEmpty(StadionID) || string.IsNullOrEmpty(ZitplaatsId))
            {
                return BadRequest();
            }

            var cart = HttpContext.Session.GetObject<List<ShoppingCartAbonementItemKortVM>>("ShoppingCartAbonement")
                       ?? new List<ShoppingCartAbonementItemKortVM>();

            var zitplaats = await _zitplaatsenService.FindByIdAsync(ZitplaatsId);

            if (zitplaats == null)
            {
                return RedirectToAction("Index", "Home");
            }

            cart.Add(new ShoppingCartAbonementItemKortVM
            {
                StadionId = StadionID,
                SeizoenId = SeizoenId,
                ZitplaatsId = ZitplaatsId

            });

            HttpContext.Session.SetObject("ShoppingCartAbonement", cart);

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}