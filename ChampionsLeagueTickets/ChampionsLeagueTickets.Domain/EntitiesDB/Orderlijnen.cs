using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Orderlijnen
{
    public string OrderId { get; set; } = null!;

    public int OrderLijnNummer { get; set; }

    public string? AbonnementId { get; set; }

    public string? StadionId { get; set; }

    public string? TicketId { get; set; }

    public string? MatchId { get; set; }

    public decimal Bedrag { get; set; }

    public virtual Abonnementen? Abonnementen { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Ticket? Ticket { get; set; }
}
