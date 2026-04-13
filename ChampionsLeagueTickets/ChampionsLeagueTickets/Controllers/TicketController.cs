using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
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

        public TicketController(IMatchService matchesService, IMapper mapper, IService<VakType> vakService, IZitplaatsenService zitplaatsenService)
        {
            _matchesService = matchesService;
            _mapper = mapper;
            _vakService = vakService;
            _zitplatsenService = zitplaatsenService;
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



        public Task<IActionResult> OverzichtInfoTicket(TicketVM vm) {



            return View();

        
        }





    }
}
