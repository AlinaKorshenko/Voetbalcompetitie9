using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class AbonnementenPrijs
{
    public string StadionId { get; set; } = null!;

    public string SeizoenId { get; set; } = null!;

    public decimal Prijs { get; set; }

    public virtual Seizoenen Seizoen { get; set; } = null!;

    public virtual Stadion Stadion { get; set; } = null!;
}
