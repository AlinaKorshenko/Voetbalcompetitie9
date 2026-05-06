using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace ChampionsLeagueTickets.Services
{
    public class TeamService : IService<Team>
    {
        private readonly IDAO<Team> _teams;

        public TeamService(IDAO<Team> teams)
        {
            _teams = teams;
        }

        public Task AddAsync(Team entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Team entity)
        {
            throw new NotImplementedException();
        }

        public Task<Team?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Team>?> GetAllAsync()
        {
            try
            {
                return await _teams.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het ophalen van alle Teams is niet gelukt: ", ex);
            }
        }
    }
}
