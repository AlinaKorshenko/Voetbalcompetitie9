using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.integrations.VrijeZitplaats.DTO;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories
{
    public class ZitplaatsenDAO : IZitplaatsenDAO
    {
        private readonly FootballDbContext _dbContext;

        public ZitplaatsenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
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
                var zitplaats = await _dbContext.Zitplaatsens
                    .Include(z => z.VakNummerNavigation)
                    .FirstOrDefaultAsync(m => m.ZitplaatsId == Id);

                if(zitplaats == null)
                {
                    throw new Exception("De ingegeven zitplaats is niet gevonden");
                }

                return zitplaats;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Zitplaatsens
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype)
        {
            try
            {
                return await _dbContext.Zitplaatsens
                    .Where(zitplaats => zitplaats.StadionId == stadion.StadionId && zitplaats.VakNummer == vaktype.VakNummer)
                    .CountAsync();
            }
            catch
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public async Task<List<string>> GetRowsForMatchAndSectionAsync(string matchId, string vakNummer)
        {
            try
            {
                var match = await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                    .FirstOrDefaultAsync(m => m.MatchId == matchId);

                if (match == null)
                {
                    throw new Exception("Match niet gevonden.");
                }

                var stadionId = match.ThuisTeam.StadionId;

                var rijen = await _dbContext.Zitplaatsens
                    .Where(z => z.StadionId == stadionId && z.VakNummer == vakNummer)
                    .Select(z => z.RijNummer)
                    .Distinct()
                    .ToListAsync();

                rijen = rijen
                    .OrderBy(r => ParseNumber(r))
                    .ToList();

                return rijen;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

      

        public async Task<List<ZitplaatsDto>> GetSeatsForMatchSectionAndRowAsync(string matchId, string vakNummer, string rijNummer)
        {
            try
            {
                var match = await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                    .FirstOrDefaultAsync(m => m.MatchId == matchId);

                if (match == null)
                {
                    throw new Exception("Match niet gevonden.");
                }

                var stadionId = match.ThuisTeam.StadionId;
                var matchDate = DateOnly.FromDateTime(match.DatumTijdStartMatch);

                var seizoen = await _dbContext.Seizoenens
                    .FirstOrDefaultAsync(s =>
                    s.StartDatum <= matchDate &&
                    s.EindDatum >= matchDate);

                var seizoenId = seizoen?.SeizoenId;

                var seats = await _dbContext.Zitplaatsens
                    .Where(z =>
                        z.StadionId == stadionId &&
                        z.VakNummer == vakNummer &&
                        z.RijNummer == rijNummer)
                    .Select(z => new ZitplaatsDto
                    {
                        ZitplaatsId = z.ZitplaatsId,
                        VakNummer = z.VakNummer,
                        RijNummer = z.RijNummer,
                        StoelNummer = z.StoelNummer,
                        IsBezet =
                            (seizoenId != null &&
                             _dbContext.Abonnementens.Any(a =>
                                a.ZitplaatsId == z.ZitplaatsId &&
                                a.SeizoenId == seizoenId &&
                                _dbContext.Orderlijnens.Any(ol => ol.AbonnementId == a.AbonnementId)))
                            ||
                            _dbContext.Tickets.Any(t =>
                                t.MatchId == matchId &&
                                t.ZitplaatsId == z.ZitplaatsId &&
                                _dbContext.Orderlijnens.Any(ol => ol.TicketId == t.TicketId))
                    })
                    .ToListAsync();

                seats = seats
                    .OrderBy(z => ParseNumber(z.StoelNummer))
                    .ToList();

                return seats;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        private static int ParseNumber(string value)
        {
            return int.TryParse(value, out int nummer) ? nummer : int.MaxValue;
        }

        public Task UpdateAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }
    }
}
