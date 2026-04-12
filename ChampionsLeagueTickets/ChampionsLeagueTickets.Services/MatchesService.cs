using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
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

        public Task AddAsync(Match entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Match entity)
        {
            throw new NotImplementedException();
        }

        public Task<Match?> FindByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {

            return await _MatchesDAO.GetAllAsync();

        }

        public Task UpdateAsync(Match entity)
        {
            throw new NotImplementedException();
        }
    }
}
