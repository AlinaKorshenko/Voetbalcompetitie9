using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class TicketsPrijsService : ITicketPrijsService
    {

        private readonly ITicketPrijsDAO _ticketPrijsDAO;

        public TicketsPrijsService(ITicketPrijsDAO ticketPrijsDAO)
        {
            _ticketPrijsDAO = ticketPrijsDAO;
        }

        public Task AddAsync(ZitplaatsenService entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ZitplaatsenService entity)
        {
            throw new NotImplementedException();
        }

        public Task<ZitplaatsenService?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ZitplaatsenService>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer)
        {
            try
            {
                if(MatchID == null)
                {
                    throw new ArgumentNullException("Het meegegeven matchId mag niet null zijn");
                }

                if (VakNummer == null)
                {
                    throw new ArgumentNullException("Het meegegeven vakNummer mag niet null zijn");
                }

                return _ticketPrijsDAO.GetTicketPrijsByMatchAndSectionAsync(MatchID, VakNummer);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is niet gelukt om de ticketprijs te vinden van meegegeven matchId '{MatchID}' en meegegeven vakNummer '{VakNummer}'", ex);
            }
        }
    }
}
