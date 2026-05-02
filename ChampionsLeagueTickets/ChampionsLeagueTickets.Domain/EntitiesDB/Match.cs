using System;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.EntitiesDB;

public partial class Match
{
    public string MatchId { get; set; } = null!;

    public string SeizoenId { get; set; } = null!;

    public string ThuisTeamId { get; set; } = null!;

    public string BezoekendTeamId { get; set; } = null!;

    public DateTime DatumTijdStartMatch { get; set; }

    public virtual Team BezoekendTeam { get; set; } = null!;

    public virtual Seizoenen Seizoen { get; set; } = null!;

    public virtual Team ThuisTeam { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<TicketsPrijs> TicketsPrijs { get; set; } = new List<TicketsPrijs>();
}
