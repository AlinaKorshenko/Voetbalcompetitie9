using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface ITicketService : IService<Ticket>
    {
        Task<int> GetAantalTicketsVoorMatchEnUser(string userId, string matchId);

        Task<bool> HasTicketOnSameDay(string userId, string matchId, DateTime matchDatum);

        Task<string> GenerateNextTicketIdAsync();
    }
}
