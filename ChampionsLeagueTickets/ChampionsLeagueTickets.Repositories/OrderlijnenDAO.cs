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
    public class OrderLijnenDAO : IOrderLijnenDAO
    {

        private readonly FootballDbContext _dbContext;

        public OrderLijnenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Orderlijnen entity)
        {
            await _dbContext.Orderlijnens.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Orderlijnen entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var trackedEntity = _dbContext.Orderlijnens.Local
                .FirstOrDefault(e => e.OrderId == entity.OrderId
                                  && e.OrderLijnNummer == entity.OrderLijnNummer);

            if (trackedEntity != null)
            {
                _dbContext.Orderlijnens.Remove(trackedEntity);
            }
            else
            {
                _dbContext.Orderlijnens.Attach(entity);
                _dbContext.Orderlijnens.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public Task<Orderlijnen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orderlijnen>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Orderlijnen> FindByOrderIdAndOrderLijnNumber(string orderId, int orderLijnNumber)
        {
            var orderlijnen = _dbContext.Orderlijnens
                .AsNoTracking()
                .Include(o => o.Ticket)
                .Include(o => o.Abonnementen)
                    .FirstOrDefaultAsync(l => l.OrderId == orderId && l.OrderLijnNummer == orderLijnNumber);

            return orderlijnen;
        }
    }
}
