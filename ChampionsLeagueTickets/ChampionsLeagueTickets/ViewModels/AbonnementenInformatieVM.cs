namespace ChampionsLeagueTickets.ViewModels
{
    public class AbonnementenInformatieVM
    {
        public string StadionNaam { get; set; }

        public string SeizoenNaam { get; set; }

        public DateOnly StartDatum { get; set; }

        public DateOnly EindDatum { get; set; }

        public decimal Prijs { get; set; }
    }
}
