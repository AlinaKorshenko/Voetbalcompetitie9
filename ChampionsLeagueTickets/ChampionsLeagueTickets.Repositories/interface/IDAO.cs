using ChampionsLeagueTickets.Domain.DataDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interface
{
    public interface IDAO<T> where T: class
    {

        public Task<IEnumerable<T>?> GetAllAsync();

    }
}
