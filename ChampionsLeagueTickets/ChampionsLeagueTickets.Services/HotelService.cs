using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class HotelService : IHotelService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public HotelService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["HotelAPI:BaseUrl"];
            _apiKey = _configuration["HotelAPI:ApiKey"];
        }

        public async Task<List<HotelItem>?> GetHotelsFromCountryAndCity(string country, string city)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{_baseUrl}?city={city}&country={country}";

                    var request = new HttpRequestMessage(HttpMethod.Get, url);

                    request.Headers.Add("X-API-KEY", _apiKey);

                    var response = await httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var root = JsonConvert.DeserializeObject<HotelAPIDTO>(apiResponse);

                    var hotels = root?.Data ?? new List<HotelItem>();

                    return hotels;
                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
