namespace ChampionsLeagueTickets.ViewModels
{
    public class ShoppingCartVM
    {
        public List<ShoppingCartItemVM>? Items { get; set; }
    }

    public class ShoppingCartItemVM {

        public string TicketID { get; set; }
        public string MatchId { get; set; }
        public string ZitplaatsId { get; set; }

            public string ThuisTeam { get; set; }
            public string UitTeam { get; set; }
        public DateTime MatchDateTime { get; set; }

        public string Stadion { get; set; }
        public string Vak { get; set; }
        public int Rij { get; set; }
        public int Stoel { get; set; }

    }
}
