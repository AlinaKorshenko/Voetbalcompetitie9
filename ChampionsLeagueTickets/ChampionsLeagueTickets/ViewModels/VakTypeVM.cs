using ChampionsLeagueTickets.Domain.EntitiesDB;

namespace ChampionsLeagueTickets.ViewModels
{
    public class VakTypeVM
    {
        public string VakNummer { get; set; } = null!;

        public int Ring { get; set; }

        public string? Omschrijving { get; set; }

    }
}
