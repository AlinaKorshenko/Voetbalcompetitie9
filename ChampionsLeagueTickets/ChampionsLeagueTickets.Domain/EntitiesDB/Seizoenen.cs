using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Seizoenen
{
    public string SeizoenId { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public DateOnly StartDatum { get; set; }

    public DateOnly EindDatum { get; set; }

    public virtual ICollection<AbonnementenPrijs> AbonnementenPrijs { get; set; } = new List<AbonnementenPrijs>();

    public virtual ICollection<Abonnementen> Abonnementens { get; set; } = new List<Abonnementen>();
}
