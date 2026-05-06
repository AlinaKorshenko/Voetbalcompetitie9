using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IAbonementenPrijsService : IService<AbonnementenPrijs>
    {
        Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId);

        Task<IEnumerable<AbonnementenPrijs>?> GetAllPrijzenFromNextSeasons();
    }
}
