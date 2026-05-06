using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
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
    public class AbonnementenService : IAbonnementService
    {
        private IAbonnementDAO _abonnementenDAO;

        public AbonnementenService(IAbonnementDAO abonnementenDAO)
        {
            _abonnementenDAO = abonnementenDAO;
        }

        public async Task AddAsync(Abonnementen entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Abonnement mag niet null zijn.");
                }

                await _abonnementenDAO.AddAsync(entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Het toevoegen van het Abonnement is mislukt: ", ex);
            }
        }

        public async Task DeleteAsync(Abonnementen entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Abonnement mag niet null zijn.");
                }

                await _abonnementenDAO.DeleteAsync(entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Het verwijderen van het Abonnement is mislukt: ", ex);
            }
        }

        public Task<Abonnementen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextAbonnementenIdAsync()
        {
            try
            {
                return await _abonnementenDAO.GenerateNextAbonnementenIdAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het genereren van het volgende abonnementenId is mislukt: ", ex);
            }
        }

        public async Task<IEnumerable<Abonnementen>?> GetAllAsync()
        {
            try
            {
                return await _abonnementenDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het genereren van het volgende abonnementenId is mislukt: ", ex);
            }
        }
    }
}
