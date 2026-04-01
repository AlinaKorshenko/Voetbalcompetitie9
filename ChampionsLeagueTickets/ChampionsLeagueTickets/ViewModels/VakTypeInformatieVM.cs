using ChampionsLeagueTickets.Domain.EntitiesDB;

namespace ChampionsLeagueTickets.ViewModels
{
    public class VakTypeInformatieVM
    {
        public string VakNummer { get; set; } = null!;

        public int Ring { get; set; }

        public string? Omschrijving { get; set; }

        public int AantalZitplaatsen { get; set; }

    }
}
