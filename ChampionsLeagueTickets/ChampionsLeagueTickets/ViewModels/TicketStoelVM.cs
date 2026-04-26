using ChampionsLeagueTickets.Domain.EntitiesDB;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ChampionsLeagueTickets.View_Models
{
    public class TicketStoelVM
    {
        [Required]
        public string MatchID { get; set; }
        public decimal? Prijs { get; set; }

        public string? StadionVak { get; set; }
        public string? RijNummer { get; set; }
        public string? GeselecteerdeZitplaatsId { get; set; }

        public SelectList? VakenLijst { get; set; }
        public SelectList? RijenLijst { get; set; }
        public SelectList? StoelenLijst { get; set; }
    }
}
