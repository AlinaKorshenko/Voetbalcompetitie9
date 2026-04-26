using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface IAbonementenPrijsDAO : IDAO<AbonnementenPrijs>
    {

        Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId);

    }
}
