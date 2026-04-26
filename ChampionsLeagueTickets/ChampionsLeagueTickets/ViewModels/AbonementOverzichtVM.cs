using ChampionsLeagueTickets.Domain.EntitiesDB;
using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTickets.ViewModels
{
    public class AbonementOverzichtVM
    {

        [Required]
        public string SeizoenId { get; set; }
        [Required]
        public string StadionID { get; set; }
        public string StadionNaam { get; set; }
        public  Zitplaatsen zitplaats { get; set; }

        public string SeizoenNaam { get; set; }
        public DateOnly StartDatum { get; set; }

        public DateOnly EindDatum { get; set; }

        public decimal Prijs { get; set; }


    }
}
