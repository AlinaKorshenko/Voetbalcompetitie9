namespace ChampionsLeagueTickets.ViewModels
{
    public class AbonnementenInformatieVM
    {
        public string StadionNaam { get; set; }
        public string SeizoenNaam { get; set; }
        public string VakNummer { get; set; }
        public string VakNaam { get; set; }
        public string SeizoenId { get; set; }
        public string StadionId { get; set; }
        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public decimal Prijs { get; set; }
        public bool IsKoopbaar { get; set; }
    }
}
