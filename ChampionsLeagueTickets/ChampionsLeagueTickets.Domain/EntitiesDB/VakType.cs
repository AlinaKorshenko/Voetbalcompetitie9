using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class VakType
{
    public string VakNummer { get; set; } = null!;

    public int Ring { get; set; }

    public string? Omschrijving { get; set; }

    public virtual ICollection<AbonnementenPrijs> AbonnementenPrijs { get; set; } = new List<AbonnementenPrijs>();

    public virtual ICollection<TicketsPrij> TicketsPrijs { get; set; } = new List<TicketsPrij>();

    public virtual ICollection<Zitplaatsen> Zitplaatsens { get; set; } = new List<Zitplaatsen>();
}
