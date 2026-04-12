using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;

namespace ChampionsLeagueTickets.View_Models
{
    public class TicketVM
    {

        public string StadionVak { get; set; }
        public string Stoel { get; set; }
        public List<SelectListItem> VakenLijst { get; set; }




    }
}
