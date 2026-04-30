using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IAbonnementService :IService<Abonnementen>
    {

        Task<string> FindAbinementIdByStadonSeizoenZitplaatsAsynk(string stadionId, string seizoenId, string zitplaatsId);
        Task<Abonnementen> FindAbonnementByStadionIdAndAbonnementId(string abonnementId, string stadionId);
        Task<string> GenerateNextAbonnementenIdAsync();
    }
}
