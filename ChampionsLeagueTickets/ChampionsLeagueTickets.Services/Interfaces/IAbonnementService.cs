using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IAbonnementService :IService<Abonnementen>
    {
        Task<string> GenerateNextAbonnementenIdAsync();
    }
}
