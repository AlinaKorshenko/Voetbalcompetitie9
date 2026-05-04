using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.ViewModels.Zitplaatsen;
using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTickets.ViewModels.ShoppingCart
{
    public class AbonementOverzichtVM
    {
        public int MyProperty { get; set; }

        [Required]
        public string SeizoenId { get; set; }
        [Required]
        public string StadionID { get; set; }
        public string StadionNaam { get; set; }
        public ZitplaatsVM zitplaats { get; set; }

        public string SeizoenNaam { get; set; }
        public DateOnly StartDatum { get; set; }

        public DateOnly EindDatum { get; set; }

        public decimal Prijs { get; set; }


    }
}
