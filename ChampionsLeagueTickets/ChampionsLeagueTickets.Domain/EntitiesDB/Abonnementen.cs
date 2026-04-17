using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Abonnementen
{
    public string AbonnementId { get; set; } = null!;

    public string StadionId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ZitplaatsId { get; set; } = null!;

    public DateOnly StartDatum { get; set; }

    public DateOnly EindDatum { get; set; }

    public bool Status { get; set; }

    public string SeizoenId { get; set; } = null!;

    public virtual ICollection<Orderlijnen> Orderlijnens { get; set; } = new List<Orderlijnen>();

    public virtual Seizoenen Seizoen { get; set; } = null!;

    public virtual Stadion Stadion { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;

    public virtual Zitplaatsen Zitplaatsen { get; set; } = null!;
}
