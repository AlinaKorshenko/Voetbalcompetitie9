using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Ticket
{
    public string TicketId { get; set; } = null!;

    public string MatchId { get; set; } = null!;

    public string ZitplaatsId { get; set; } = null!;

    public string StadionId { get; set; } = null!;

    public decimal Prijs { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual ICollection<Orderlijnen> Orderlijnens { get; set; } = new List<Orderlijnen>();

    public virtual Stadion Stadion { get; set; } = null!;

    public virtual Zitplaatsen Zitplaatsen { get; set; } = null!;
}
