namespace ChampionsLeagueTickets.ViewModels.order
{
    public class OrderLijnVM
    {

        public int orderLijnNummer { get; set; }

        public OrderTicketVM? Ticket { get; set; }
        public OrderAbonementVM? Abonement { get; set; }
        public double bedrag { get; set; }


    }
}
