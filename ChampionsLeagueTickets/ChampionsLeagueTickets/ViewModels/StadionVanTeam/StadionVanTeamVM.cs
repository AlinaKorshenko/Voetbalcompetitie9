namespace ChampionsLeagueTickets.ViewModels.StadionVanTeamVM
{
    public class StadionVanTeamVM
    {

        public string TeamNaam { get; set; }

        public string StadionNaam { get; set; }
        public string Land { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public int AantalZitplaatsen { get; set; }

        public List<VakTypeVanStadionVM> VakTypes { get; set; } = new();




    }
}
