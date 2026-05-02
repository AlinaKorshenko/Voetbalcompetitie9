using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeagueTickets.Controllers
{
    public class HotelController : Controller
    {
        private readonly IDAO<Stadion> _stadionDAO;
        private readonly IHotelService _hotelService;

        public HotelController(IDAO<Stadion> stadionDAO, IHotelService hotelService)
        {
            _stadionDAO = stadionDAO;
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Index()
        {
            var stadiums = await _stadionDAO.GetAllAsync();

            var vm = new HotelVM
            {
                Stadiums = new SelectList(stadiums, nameof(Stadion.StadionId), nameof(Stadion.Naam))
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string selectedStadiumId)
        {
            var stadiums = await _stadionDAO.GetAllAsync();
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
