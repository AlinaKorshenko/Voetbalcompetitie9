using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface IDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T?> FindByIdAsync(string Id);
    }
}
