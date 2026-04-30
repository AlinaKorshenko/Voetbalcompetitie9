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


    public class OrderDAO : IOrderDAO
    {

        private readonly FootballDbContext _dbContext;

        public OrderDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<Order?> FindByIdAsync(string Id)
        {
            return await _dbContext.Orders
       .FirstOrDefaultAsync(o => o.OrderId == Id);
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllByUserId(string userId)
        {
            return await _dbContext.Orders
         .Where(o => o.UserId == userId)

         .Include(o => o.Orderlijnens)
             .ThenInclude(ol => ol.Ticket)
                 .ThenInclude(t => t.Zitplaatsen)

         .Include(o => o.Orderlijnens)
             .ThenInclude(ol => ol.Abonnementen)
                 .ThenInclude(a => a.Zitplaatsen)
         .Include(o => o.Orderlijnens)
             .ThenInclude(ol => ol.Abonnementen)
             .ThenInclude(a => a.Seizoen)


         .ToListAsync();

        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
