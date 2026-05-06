using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTickets.Repositories
{
    public class OrderDAO : IOrderDAO
    {
        private readonly FootballDbContext _dbContext;

        public OrderDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order entity)
        {
            try
            {
                await _dbContext.Orders.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<Order?> FindByIdAsync(string id)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Orderlijnens)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    throw new Exception("Order not found for the given OrderId.");
                }

                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<string> GenerateNextOrderIdAsync()
        {
            try
            {
                var lastOrder = await _dbContext.Orders
                    .OrderByDescending(o => o.OrderId)
                    .FirstOrDefaultAsync();

                if (lastOrder == null)
                    return "O0001";

                var lastNumber = int.Parse(lastOrder.OrderId.Substring(1));
                var newNumber = lastNumber + 1;

                return $"O{newNumber.ToString("D4")}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Order>> GetAllByUserId(string userId)
        {
            try
            {
                var orders = await _dbContext.Orders
                    .AsNoTracking()
                    .Where(o => o.UserId == userId)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Ticket)
                            .ThenInclude(t => t.Zitplaatsen)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Ticket)
                            .ThenInclude(t => t.Match)
                                .ThenInclude(m => m.ThuisTeam.Stadion)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Ticket)
                            .ThenInclude(t => t.Match.BezoekendTeam)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Abonnementen)
                            .ThenInclude(a => a.Zitplaatsen)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Abonnementen)
                            .ThenInclude(a => a.Seizoen)
                    .Include(o => o.Orderlijnens)
                        .ThenInclude(ol => ol.Abonnementen)
                            .ThenInclude(a => a.Stadion)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Order entity)
        {
            try
            {
                _dbContext.Orders.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }
    }
}