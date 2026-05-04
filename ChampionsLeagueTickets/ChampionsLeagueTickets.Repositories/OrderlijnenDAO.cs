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
            try
            {
                await _dbContext.Orderlijnens.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task DeleteAsync(Orderlijnen entity)
        {
            throw new NotImplementedException();
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
            return _dbContext.Orderlijnens
                .Include(o => o.Ticket)
                .Include(o => o.Abonnementen)
                 .FirstOrDefaultAsync(l => l.OrderId == orderId && l.OrderLijnNummer == orderLijnNumber);
        }
    }
}
