using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface IOrderDAO : IDAO<Order>
    {

        Task<List<Order>> GetAllByUserId(string userId);


        Task<IEnumerable<Order>?> GetAllOrderInformationFromUser(string userId);
        Task<string> GenerateNextOrderIdAsync();
    }
}
