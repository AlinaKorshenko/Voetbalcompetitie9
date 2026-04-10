using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class HotelService : IHotelService
    {
        private IConfiguration _configure;
        private string? apiBaseUrl;

        public HotelService(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure["WebAPIBaseUrl"];
        }

        public Task<List<HotelItem>?> GetHotelsFromCountryAndCity(string country, string city)
        {
            throw new NotImplementedException();
        }
    }
}
