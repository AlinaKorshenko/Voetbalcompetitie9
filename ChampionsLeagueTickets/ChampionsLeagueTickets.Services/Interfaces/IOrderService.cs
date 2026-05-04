using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IOrderService : IService<Order>
    {
        Task<List<Order>> GetAllByUserId(string userId);

        Task<string> GenerateNextOrderIdAsync();
    }
}
