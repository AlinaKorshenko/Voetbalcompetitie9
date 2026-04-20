namespace ChampionsLeagueTickets.ViewModels
{


    public class ShoppingCartVM {
        public List<ShoppingCartTicketVM> Tickets { get; set; } = new();
    }



    public class ShoppingCartItemKortVM
    {
        public string MatchId { get; set; }
        public string ZitplaatsId { get; set; }
        public int Aantal { get; set; }
        public string VakNummer{ get; set; }
    }

        public class ShoppingCartTicketVM {

            public string ThuisTeam { get; set; }
            public string UitTeam { get; set; }
        public DateTime MatchDateTime { get; set; }
        public decimal Prijs { get; set; }

        public string Stadion { get; set; }
        public string Vak { get; set; }
        public string Rij { get; set; }
        public string Stoel { get; set; }
        public int Aantal { get; set; }

    }
}
