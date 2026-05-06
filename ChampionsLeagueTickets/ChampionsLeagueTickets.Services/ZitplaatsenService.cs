using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class ZitplaatsenService : IZitplaatsenService
    {
        private IZitplaatsenDAO _zitplaatsenDAO;

        public ZitplaatsenService(IZitplaatsenDAO zitplaatsenDAO)
        {
            _zitplaatsenDAO = zitplaatsenDAO;
        }

        public Task AddAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Zitplaatsen?> FindByIdAsync(string Id)
        {
            try
            {
                if(Id == null)
                {
                    throw new ArgumentNullException("Het meegegeven zitplaatsId mag niet null zijn.");
                }

                return await _zitplaatsenDAO.FindByIdAsync(Id);
            }
            catch(Exception ex)
            {
                throw new Exception("Het is niet gelukt om de Zitplaats op te halen voor meegegeven zitplaatsId '{Id}': ", ex);
            }
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            try
            {
                return await _zitplaatsenDAO.GetAllAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Het is niet gelukt om alle Zitplaatsen op te halen: ", ex);
            }
        }

        public async Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype)
        {
            try
            {
                if(stadion == null)
                {
                    throw new ArgumentNullException("Het meegegeven Stadion mag niet null zijn.");
                }

                if (vaktype == null)
                {
                    throw new ArgumentNullException("Het meegegeven VakType mag niet null zijn.");
                }

                return await _zitplaatsenDAO.GetCountZitplaatsenByVakTypeAndStadion(stadion, vaktype);
            }
            catch(Exception ex)
            {
                throw new Exception($"Het is niet gelukt om het aantal zitplaatsen per Stadion '{stadion}' en VakType '{vaktype}' op te halen.", ex);
            }
        }

        public Task<List<string>> GetRowsForSectionAsync(string stadionID, string vakNummer)
        {
            try
            {
                if (stadionID == null)
                {
                    throw new ArgumentNullException("Het meegegeven stadionId mag niet null zijn.");
                }

                if (vakNummer == null)
                {
                    throw new ArgumentNullException("Het meegegeven vakNummer mag niet null zijn.");
                }

                return _zitplaatsenDAO.GetRowsForSectionAsync(stadionID, vakNummer);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is niet gelukt om de Zitplaatsen op te halen voor stadionId '{stadionID}' en vakNummer '{vakNummer}': ", ex);
            }
        }

        public async Task<List<ZitplaatsDto>> GetSeatsForMatchSectionAndRowAsync(
            string matchId, string vakNummer, string rijNummer)
        {
            try
            {
                if (matchId == null)
                {
                    throw new ArgumentNullException("Het meegegeven mathcId mag niet null zijn.");
                }

                if (vakNummer == null)
                {
                    throw new ArgumentNullException("Het meegegeven vakNummer mag niet null zijn.");
                }

                if (rijNummer == null)
                {
                    throw new ArgumentNullException("Het meegegeven rijNummer mag niet null zijn.");
                }

                var rawSeats = await _zitplaatsenDAO.GetSeatsForMatchSectionAndRowAsync(
                matchId, vakNummer, rijNummer);

                return rawSeats.Select(s => new ZitplaatsDto
                {
                    ZitplaatsId = s.Zitplaats.ZitplaatsId,
                    VakNummer = s.Zitplaats.VakNummer,
                    RijNummer = s.Zitplaats.RijNummer,
                    StoelNummer = s.Zitplaats.StoelNummer,
                    IsBezet = s.IsBezet
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Het weergeven van de zitplaatsen is niet gelukt voor matchId '{matchId}', vakNummer '{vakNummer}' en rijNummer '{rijNummer}'", ex);
            }
        }

        public async Task<List<Zitplaatsen>> GetFreeSeatsForSeasonSectionAndRowAsync(string stadionId, string seizoenId, string vakNummer, string rijNummer)
        {
            return await _zitplaatsenDAO.GetFreeSeatsForSeasonSectionAndRowAsync(stadionId, seizoenId, vakNummer, rijNummer);
        }

        public Task<List<Zitplaatsen>> GetByStadionIdAsync(string stadionId)
        {

            try
            {

                if (stadionId == null)
                {
                    throw new ArgumentNullException("Het meegegeven stadionId mag niet null zijn.");
                }
                return _zitplaatsenDAO.GetByStadionIdAsync(stadionId);
            }
            catch {
                throw new Exception($"Het is niet gelukt om de Zitplaatsen op te halen voor stadionId '{stadionId}'");
            }
            
        }
    }
}
