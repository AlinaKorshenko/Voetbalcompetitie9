using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Team
{
    public string TeamId { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string StadionId { get; set; } = null!;

    public virtual ICollection<Match> MatchBezoekendTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchThuisTeams { get; set; } = new List<Match>();

    public virtual Stadion Stadion { get; set; } = null!;
}
