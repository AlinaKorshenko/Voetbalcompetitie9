using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeagueTickets.Controllers
{
    public class HotelController : Controller
    {
        private readonly IService<Stadion> _stadionService;
        private readonly IHotelService _hotelService;

        public HotelController(IService<Stadion> stadionService, IHotelService hotelService)
        {
            _stadionService = stadionService;
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stadiums = await _stadionService.GetAllAsync();

            var vm = new HotelVM
            {
                Stadiums = new SelectList(stadiums, nameof(Stadion.StadionId), nameof(Stadion.Naam))
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string selectedStadiumId)
        {
            var stadiums = await _stadionService.GetAllAsync();
            var stadiumList = stadiums?.ToList() ?? [];
            var selected = stadiumList.FirstOrDefault(s => s.StadionId == selectedStadiumId);

            var vm = new HotelVM
            {
                Stadiums = new SelectList(stadiumList, nameof(Stadion.StadionId), nameof(Stadion.Naam), selectedStadiumId),
                SelectedStadiumId = selectedStadiumId,
                SelectedStadiumName = selected?.Naam
            };

            if (selected != null)
            {
                var result = await _hotelService.GetNearbyHotelsAsync(selected.Latitude, selected.Longitude);
                vm.Hotels = result?.Results;
            }

            return View(vm);
        }
    }
}
