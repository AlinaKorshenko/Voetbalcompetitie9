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
    internal class AbonnementenPrijsService : IService<AbonnementenPrijs>
    {
        private IDAO<AbonnementenPrijs> _abonnementenPrijsDAO;

        public AbonnementenPrijsService(IDAO<AbonnementenPrijs> abonnementenPrijsDAO)
        {
            _abonnementenPrijsDAO = abonnementenPrijsDAO;
        }

        public Task AddAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task<AbonnementenPrijs?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AbonnementenPrijs>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }
    }
}
