using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories
{
    public class MatchesDAO : IDAO<Match>
    {

        private readonly FootballDbContext _dbContext;

        public MatchesDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Match>?> GetAllAsync()
        {
            return await _dbContext.Matches
                .Include(m => m.ThuisTeam)
                    .ThenInclude(t => t.Stadion)
                .Include(m => m.BezoekendTeam)
                .Where(m => m.DatumTijdStartMatch > DateTime.Now)
                .ToListAsync();
        }

    }
}
