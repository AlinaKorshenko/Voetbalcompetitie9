using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Zitplaatsen
{
    public string StadionId { get; set; } = null!;

    public string ZitplaatsId { get; set; } = null!;

    public string VakNummer { get; set; } = null!;

    public string RijNummer { get; set; } = null!;

    public string StoelNummer { get; set; } = null!;

    public virtual ICollection<Abonnementen> Abonnementens { get; set; } = new List<Abonnementen>();

    public virtual Stadion Stadion { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual VakType VakNummerNavigation { get; set; } = null!;
}
