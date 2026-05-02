using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Integrations.HotelsAPI
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string BaseUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";

        public HotelService(HttpClient httpClient, IConfiguration _configuration)
        {
            _httpClient = httpClient;
            _configuration = _configuration;
        }

        public async Task<GoogleHotelApiDTO?> GetNearbyHotelsAsync(double lat, double lng, int radiusMeters = 5000)
        {
            var apiKey = _configuration["GoogleHotelAPIKey"];
            var url = $"{BaseUrl}?location={lat},{lng}&radius={radiusMeters}&type=lodging&key={apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GoogleHotelApiDTO>(json);

            if (result?.Status != "OK" && result?.Status != "ZERO_RESULTS")
                throw new Exception($"Google Places API error: {result?.Status}");

            return result;
        }
    }
}
