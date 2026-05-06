using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace ChampionsLeagueTickets.Services
{
    public class TicketService : ITicketService
    {

        private ITicketDAO _ticketsDAO;

        public TicketService(ITicketDAO ticketsDAO)
        {
            _ticketsDAO = ticketsDAO;
        }
        public async Task AddAsync(Ticket entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Ticket mag niet null zijn.");
                }

                await _ticketsDAO.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Het is niet gelukt om het Ticket toe te voegen: ", ex);
            }
        }

        public async Task DeleteAsync(Ticket entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Ticket mag niet null zijn.");
                }

                await _ticketsDAO.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Het is niet gelukt om het Ticket toe te verwijderen: ", ex);
            }
        }

        public Task<Ticket?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextTicketIdAsync()
        {
            try
            {
                return await _ticketsDAO.GenerateNextTicketIdAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het genereren van het volgende ticketId is niet gelukt: ", ex);
            }
        }

        public async Task<int> GetAantalTicketsVoorMatchEnUser(string userId, string matchId)
        {
            try
            {
                if (userId == null)
                {
                    throw new ArgumentNullException("Het meegegeven ticketId mag niet null zijn.");
                }

                if (matchId == null)
                {
                    throw new ArgumentNullException("Het meegegeven matchId mag niet null zijn.");
                }

                return await _ticketsDAO.GetAantalTicketsVoorMatchEnUser(userId, matchId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is niet gelukt om de Tickets te vinden voor userId '{userId}' en matchId '{matchId}': ", ex);
            }
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            try
            {
                return await _ticketsDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het is niet gelukt om alle Tickets te vinden");
            }
        }

        public async Task<bool> HasTicketOnSameDay(string userId, string matchId, DateTime matchDatum)
        {
            try
            {
                return await _ticketsDAO.HasTicketOnSameDay(userId, matchId, matchDatum);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"Het is niet gelukt om de verificatie te doen voor het meegegeven userId '{userId}', matchId '{matchId}' en matchDatum '{matchDatum}': ", ex);
                }
            }
        }
    }
}
