using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels.Abonnementen;
using ChampionsLeagueTickets.ViewModels.order;
using ChampionsLeagueTickets.ViewModels.ShoppingCart;
using ChampionsLeagueTickets.ViewModels.Zitplaatsen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeagueTickets.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAbonementenPrijsService _abonnementenPrijsService;
        private readonly IService<Seizoenen> _seizoenenService;
        private readonly IZitplaatsenService _zitplaatsenService;
        private readonly IService<VakType> _vakService;
        private readonly IService<Stadion> _stadionService;

        public AbonnementController(IMapper mapper, IAbonementenPrijsService abonnementenPrijsService, IService<Seizoenen> seizoenenService, IZitplaatsenService zitplaatsenService, IService<VakType> vakCervice, IService<Stadion> stadionService)
        {
            _mapper = mapper;
            _abonnementenPrijsService = abonnementenPrijsService;
            _seizoenenService = seizoenenService;
            _zitplaatsenService = zitplaatsenService;
            _vakService = vakCervice;
            _stadionService = stadionService;
        }

        public async Task<IActionResult> Index(string? seizoenId = null)
        {
            var seizoenen = await _seizoenenService.GetAllAsync();

            var abonnementenPrijzen = await _abonnementenPrijsService.GetAllAsync();

            if (!string.IsNullOrEmpty(seizoenId))
            {
                abonnementenPrijzen = abonnementenPrijzen.Where(a => a.SeizoenId == seizoenId);
            }

            var abonnementenVm = _mapper.Map<IEnumerable<AbonnementenInformatieVM>>(abonnementenPrijzen);

            foreach (var abonnement in abonnementenVm)
            {
                abonnement.IsKoopbaar = IsAbonnementBeschikbaar(abonnement.StartDatum);
            }

            var vm = new AbonnementenIndexVM
            {
                Abonnementen = abonnementenVm,
                GeselecteerdSeizoenId = seizoenId,
                SeizoenenLijst = new SelectList(
                    seizoenen.OrderBy(s => s.StartDatum),
                    "SeizoenId",
                    "Naam",
                    seizoenId
                )
            };

            return View(vm);
        }

        private bool IsAbonnementBeschikbaar(DateOnly startDatum)
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Now);

            return vandaag < startDatum;
        }

        public async Task<IActionResult> ChooseSeats(string seizoenId, string stadionId, string vakNummer)
        {
            if (string.IsNullOrWhiteSpace(seizoenId) ||
                string.IsNullOrWhiteSpace(stadionId))
            {
                return BadRequest();
            }

            var vakken = await _vakService.GetAllAsync();

            // ✅ If vakNummer missing or invalid → fallback to first valid
            var vak = vakken.FirstOrDefault(v => v.VakNummer == vakNummer)
                      ?? vakken.First();

            // 🔥 IMPORTANT: always rebuild display name from DB
            var rijen = await _zitplaatsenService.GetRowsForSectionAsync(stadionId, vak.VakNummer);

            var vm = new AbonementStoelVM
            {
                SeizoenId = seizoenId,
                StadionID = stadionId,
                VakNummer = vak.VakNummer,
                VakNaam = vak.Omschrijving, // 👈 THIS FIXES YOUR MISSING TEXT ISSUE
                RijenLijst = new SelectList(rijen)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseSeats(AbonementStoelVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var vakken = await _vakService.GetAllAsync();

            var vak = vakken.FirstOrDefault(v => v.VakNummer == model.VakNummer);

            if (vak == null)
                vak = vakken.First(); // fallback safety

            model.VakNummer = vak.VakNummer;
            model.VakNaam = vak.Omschrijving; // 🔥 FIX missing display text

            var rijen = await _zitplaatsenService.GetRowsForSectionAsync(
                model.StadionID,
                model.VakNummer);

            model.RijenLijst = new SelectList(rijen, model.RijNummer);

            if (!string.IsNullOrEmpty(model.RijNummer))
            {
                var vrijeStoelen = await _zitplaatsenService.GetFreeSeatsForSeasonSectionAndRowAsync(
                    model.StadionID,
                    model.SeizoenId,
                    model.VakNummer,
                    model.RijNummer
                );

                model.StoelenLijst = new SelectList(
                    vrijeStoelen.Select(s => new
                    {
                        ZitplaatsId = s.ZitplaatsId,
                        DisplayName = "Stoel " + s.StoelNummer
                    }),
                    "ZitplaatsId",
                    "DisplayName",
                    model.GeselecteerdeZitplaatsId
                );
            }

            return View(model);
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
                VakNummer = stoelVm.VakNummer,
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