using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories
{
    public class OrderLijnDAO : IOrderLijnDAO
    {

        private readonly FootballDbContext _dbContext;

        public OrderLijnDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task AddAsync(Orderlijnen entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Orderlijnen entity)
        {
            _dbContext.Orderlijnens.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Orderlijnen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Orderlijnen> FindByOrderIdAndOrderLijnNumber(string orderId, int orderLijnNumber)
        {
            return _dbContext.Orderlijnens
                 .FirstOrDefaultAsync(l => l.OrderId == orderId && l.OrderLijnNummer == orderLijnNumber);
        }

        public Task<IEnumerable<Orderlijnen>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Orderlijnen entity)
        {
            throw new NotImplementedException();
        }
    }
}
