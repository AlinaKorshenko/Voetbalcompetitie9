using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO
{
    public class HotelAPIDTO
    {
        [JsonProperty("succes")]
        public Boolean Succes {  get; set; }

        [JsonProperty("data")]
        public List<HotelItem> Results { get; set; } = new();

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }

    public class HotelItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


        [JsonProperty("country_code")]
        public string Country_code { get; set; }


        [JsonProperty("address")]
        public string Address { get; set; }


        [JsonProperty("rating")]
        public int Rating { get; set; }


        [JsonProperty("lat")]
        public double Lat { get; set; }


        [JsonProperty("lng")]
        public int Lng { get; set; }

        [JsonProperty("amenities")]
        public string[] Amenities { get; set; }
    }
}
