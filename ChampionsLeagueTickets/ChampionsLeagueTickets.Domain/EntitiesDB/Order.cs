using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime DatumTijdOrder { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Orderlijnen> Orderlijnens { get; set; } = new List<Orderlijnen>();

    public virtual AspNetUser User { get; set; } = null!;
}
