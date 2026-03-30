using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class MatchesService : IService<Match>
    {

        private readonly IDAO<Match> _MatchesDAO;


        public MatchesService(IDAO<Match> MatcesDAO)
        {
            _MatchesDAO = MatcesDAO;
        }
        public async Task<IEnumerable<Match>> GetAllAsync()
        {

            return await _MatchesDAO.GetAllAsync();

        }
    }
}
