using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.integrations.VrijeZitplaats.DTO;

namespace ChampionsLeagueTickets.ViewModels
{
    public class TicketInfoVM
    {

        public string BezoekTeam { get; set; }
        public string ThuisTeam { get; set; }
        public string Stadion { get; set; }
        public Zitplaatsen Zitplaats { get; set; }



    }
}
