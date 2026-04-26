using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface IAbonnementDAO : IDAO<Abonnementen>
    {

        Task<string> FindAbinementIdByStadonSeizoenZitplaatsAsynk(string stadionId, string seizoenId, string zitplaatsId);
        Task<Abonnementen> FindAbonnementByStadionIdAndAbonnementId(string abonnementId, string stadionId);

    }
}
