using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface ISeizoenenService : IService<Seizoenen>
    {
        Task<IEnumerable<Seizoenen>?> GetCurrentSeizoen();
    }
}
