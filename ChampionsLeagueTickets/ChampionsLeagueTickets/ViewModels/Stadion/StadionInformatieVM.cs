using ChampionsLeagueTickets.ViewModels.Zitplaatsen;

namespace ChampionsLeagueTickets.ViewModels.Stadion
{
    public class StadionInformatieVM
    {
        public StadionVM Stadion { get; set; }
        public List<VakTypeInformatieVM> VakTypes { get; set; }
    }
}
