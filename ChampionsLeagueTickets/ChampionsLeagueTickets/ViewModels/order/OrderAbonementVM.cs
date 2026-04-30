namespace ChampionsLeagueTickets.ViewModels.order
{
    public class OrderAbonementVM
    {
        public string? AbonnementId { get; set; }

        public string StadionId { get; set; } = "";

        public string StadionNaam { get; set; } = "";
        public string SeizoenNaam { get; set; } = "";
        public ZitplaatsVM? Zitplaats { get; set; }

        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public decimal Prijs { get; set; }
    }
}
