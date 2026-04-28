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
        Task<Ticket?> FindByMatchAndSeatAsync(string matchId, string seatId);

        Task<int> GetAantalTicketsVoorMatchEnUser(string userId, string matchId);
    }
}
