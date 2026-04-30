using ChampionsLeagueTickets.Domain.EntitiesDB;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace ChampionsLeagueTickets.ViewModels.order
{
    public class OrderVM
    {
   
            public string OrderId { get; set; } = "";
            public DateTime DatumTijdOrder { get; set; }
            public string Status { get; set; } = "";

            public List<OrderLijnVM> OrderLijnen { get; set; } = new();
     
    }
}
