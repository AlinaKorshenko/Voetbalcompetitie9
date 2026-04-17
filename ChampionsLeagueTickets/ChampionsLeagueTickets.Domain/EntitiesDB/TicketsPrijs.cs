using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class TicketsPrijs
{
    public string MatchId { get; set; } = null!;

    public string VakNummer { get; set; } = null!;

    public decimal Prijs { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual VakType VakNummerNavigation { get; set; } = null!;
}
