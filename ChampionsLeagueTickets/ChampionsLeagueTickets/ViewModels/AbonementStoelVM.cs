using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTickets.ViewModels
{
    public class AbonementStoelVM
    {
        [Required]
        public string SeizoenId { get; set; }
        [Required]
        public string StadionID { get; set; }

        public string? StadionVak { get; set; }
        public string? RijNummer { get; set; }
        public string? GeselecteerdeZitplaatsId { get; set; }

        public SelectList? VakenLijst { get; set; }
        public SelectList? RijenLijst { get; set; }
        public SelectList? StoelenLijst { get; set; }
    }
}
