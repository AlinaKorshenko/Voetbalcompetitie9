using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public interface IService<T> where T : class
    {

        public Task<IEnumerable<T>> GetAllAsync();


    }
}
