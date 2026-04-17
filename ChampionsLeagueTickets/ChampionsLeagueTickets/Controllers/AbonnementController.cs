using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<AbonnementenPrijs> _abonnementenPrijsService;
        private readonly ISeizoenenService _seizoenenService;
        private readonly IZitplaatsenService _zitplaatsenService;

        public AbonnementController(IMapper mapper, IService<AbonnementenPrijs> abonnementenPrijsService, ISeizoenenService seizoenenService, IZitplaatsenService zitplaatsenService)
        {
            _mapper = mapper;
            _abonnementenPrijsService = abonnementenPrijsService;
            _seizoenenService = seizoenenService;
            _zitplaatsenService = zitplaatsenService;
        }

        public async Task<IActionResult> Index()
        {
            var abonnementenPrijzen = await _abonnementenPrijsService.GetAllAsync();
            var vm = _mapper.Map<IEnumerable<AbonnementenInformatieVM>>(abonnementenPrijzen);
            return View(vm);
        }
    }
}
