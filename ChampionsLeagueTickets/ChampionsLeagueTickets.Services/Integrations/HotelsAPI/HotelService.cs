using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ChampionsLeagueTickets.Services.Integrations.HotelsAPI
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string BaseUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";

        public HotelService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GoogleHotelApiDTO?> GetNearbyHotelsAsync(double lat, double lng, int radiusMeters = 5000)
        {
            var apiKey = _configuration["GoogleHotelApiKey"];
            var url = $"{BaseUrl}?location={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lng.ToString(System.Globalization.CultureInfo.InvariantCulture)}&radius={radiusMeters}&type=lodging&key={apiKey}";

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