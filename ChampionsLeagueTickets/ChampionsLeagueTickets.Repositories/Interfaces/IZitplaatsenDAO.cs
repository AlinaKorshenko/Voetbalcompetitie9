using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.integrations.VrijeZitplaats.DTO;
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
        Task<List<string>> GetRowsForMatchAndSectionAsync(string matchId, string vakNummer);

        Task<List<(Zitplaatsen Zitplaats, bool IsBezet)>> GetSeatsForMatchSectionAndRowAsync(string matchId, string vakNummer, string rijNummer);
    }
}
