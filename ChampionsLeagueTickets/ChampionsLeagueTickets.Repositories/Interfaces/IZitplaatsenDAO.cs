using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IZitplaatsenDAO : IDAO<Zitplaatsen>
    {
        Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype);
    }
}
