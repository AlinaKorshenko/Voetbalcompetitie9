using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
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
            var zitplaats = await _dbContext.Zitplaatsens
                .Include(z => z.VakNummerNavigation)
                .Include(z => z.Stadion)
                .FirstOrDefaultAsync(m => m.ZitplaatsId == Id);

            return zitplaats;
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            return await _dbContext.Zitplaatsens
                .Include(z => z.VakNummerNavigation)
                .ToListAsync();
        }

        public async Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype)
        {
            return await _dbContext.Zitplaatsens
                .Where(zitplaats => zitplaats.StadionId == stadion.StadionId && zitplaats.VakNummer == vaktype.VakNummer)
                .CountAsync();
        }

        public async Task<List<string>> GetRowsForSectionAsync(string stadionID, string vakNummer)
        {
            var rijen = await _dbContext.Zitplaatsens
                .Where(z => z.StadionId == stadionID && z.VakNummer == vakNummer)
                .Select(z => z.RijNummer)
                .Distinct()
                .ToListAsync();

            rijen = rijen
                .OrderBy(r => ParseNumber(r))
                .ToList();

            return rijen;
        }


        public async Task<List<(Zitplaatsen Zitplaats, bool IsBezet)>> GetSeatsForMatchSectionAndRowAsync(
            string matchId, string vakNummer, string rijNummer)
        {
            var match = await _dbContext.Matches
                .Include(m => m.ThuisTeam)
                .FirstOrDefaultAsync(m => m.MatchId == matchId);

            var stadionId = match.ThuisTeam.StadionId;
            var matchDate = DateOnly.FromDateTime(match.DatumTijdStartMatch);

            var seizoen = await _dbContext.Seizoenens
                .FirstOrDefaultAsync(s => s.StartDatum <= matchDate && s.EindDatum >= matchDate);

            var seizoenId = seizoen?.SeizoenId;

            var seats = await _dbContext.Zitplaatsens
                .Where(z =>
                    z.StadionId == stadionId &&
                    z.VakNummer == vakNummer &&
                    z.RijNummer == rijNummer)
                .Select(z => new
                {
                    Zitplaats = z,
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

            return seats
                .OrderBy(z => ParseNumber(z.Zitplaats.StoelNummer))
                .Select(z => (z.Zitplaats, z.IsBezet))
                .ToList();
        }

        private static int ParseNumber(string value)
        {
            return int.TryParse(value, out int nummer) ? nummer : int.MaxValue;
        }

        public async Task<List<Zitplaatsen>> GetFreeSeatsForSeasonSectionAndRowAsync(
         string stadionId,
         string seizoenId,
         string vakNummer,
         string rijNummer)
        {
            var seizoen = await _dbContext.Seizoenens
                .FirstOrDefaultAsync(s => s.SeizoenId == seizoenId);

            if (seizoen == null)
                throw new Exception("Seizoen niet gevonden.");

            var matchIdsInStadionEnSeizoen = _dbContext.Matches
                .Where(m =>
                    m.ThuisTeam.StadionId == stadionId &&
                    DateOnly.FromDateTime(m.DatumTijdStartMatch) >= seizoen.StartDatum &&
                    DateOnly.FromDateTime(m.DatumTijdStartMatch) <= seizoen.EindDatum)
                .Select(m => m.MatchId);

            var seats = await _dbContext.Zitplaatsens
                .Where(z =>
                    z.StadionId == stadionId &&
                    z.VakNummer == vakNummer &&
                    z.RijNummer == rijNummer &&

                    !_dbContext.Abonnementens.Any(a =>
                        a.StadionId == stadionId &&
                        a.SeizoenId == seizoenId &&
                        a.ZitplaatsId == z.ZitplaatsId &&
                        _dbContext.Orderlijnens.Any(ol => ol.AbonnementId == a.AbonnementId))

                    &&

                    !_dbContext.Tickets.Any(t =>
                        matchIdsInStadionEnSeizoen.Contains(t.MatchId) &&
                        t.ZitplaatsId == z.ZitplaatsId &&
                        _dbContext.Orderlijnens.Any(ol => ol.TicketId == t.TicketId))
                )
                .ToListAsync();

            return seats
                .OrderBy(z => ParseNumber(z.StoelNummer))
                .ToList();
        }

        public async Task<List<Zitplaatsen>> GetByStadionIdAsync(string stadionId)
        {
            return await _dbContext.Zitplaatsens
               .Where(z => z.StadionId == stadionId)
               .Include(z => z.VakNummerNavigation)
               .ToListAsync();
        }
    }
}
