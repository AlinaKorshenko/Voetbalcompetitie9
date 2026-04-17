using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Stadion
{
    public string StadionId { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Land { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Postcode { get; set; } = null!;

    public string Gemeente { get; set; } = null!;

    public virtual ICollection<AbonnementenPrijs> AbonnementenPrijs { get; set; } = new List<AbonnementenPrijs>();

    public virtual ICollection<Abonnementen> Abonnementens { get; set; } = new List<Abonnementen>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Zitplaatsen> Zitplaatsens { get; set; } = new List<Zitplaatsen>();
}
