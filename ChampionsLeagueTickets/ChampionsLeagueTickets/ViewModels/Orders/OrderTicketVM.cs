using ChampionsLeagueTickets.ViewModels.Zitplaatsen;

namespace ChampionsLeagueTickets.ViewModels.order
{
    public class OrderTicketVM
    {
        public string TicketId { get; set; }
        public string MatchID { get; set; }
        public string BezoekTeam { get; set; }
        public string ThuisTeam { get; set; }
        public string Stadion { get; set; }
        public DateTime Datum { get; set; }
        public ZitplaatsVM Zitplaats { get; set; }
        public decimal Prijs { get; set; }

    }
}
