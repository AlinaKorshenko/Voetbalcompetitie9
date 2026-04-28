using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface IMatchDAO : IDAO<Match>
    {
        Task<IEnumerable<Match>?> GetAllMatchesFromTeamsAsync(string homeTeamId, string awayTeamId);

        Task<Stadion> GetStadionByMatchIdAsync(string matchId);
    }
}
