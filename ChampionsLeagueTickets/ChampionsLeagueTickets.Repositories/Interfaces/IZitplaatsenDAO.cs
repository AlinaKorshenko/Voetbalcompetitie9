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

        Task<List<string>> GetRowsForSectionAsync(string stadionID, string vakNummer);

        Task<List<(Zitplaatsen Zitplaats, bool IsBezet)>> GetSeatsForMatchSectionAndRowAsync(string matchId, string vakNummer, string rijNummer);

        Task<List<Zitplaatsen>> GetFreeSeatsForSeasonSectionAndRowAsync(string stadionId, string seizoenId, string vakNummer, string rijNummer);
    }
}
