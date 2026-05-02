using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO
{
    public class GoogleHotelApiDTO
    {
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("results")]
        public List<GoogleHotelItem>? Results { get; set; } = new();

        [JsonProperty("next_page_token")]
        public string? NextPageToken { get; set; }
    }

    public class GoogleHotelItem
    {
        [JsonProperty("place_id")]
        public string? PlaceId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("vicinity")]
        public string? Vicinity { get; set; }

        [JsonProperty("rating")]
        public double? Rating { get; set; }

        [JsonProperty("user_ratings_total")]
        public int? UserRatingsTotal { get; set; }

        [JsonProperty("price_level")]
        public int? PriceLevel { get; set; }

        [JsonProperty("geometry")]
        public GoogleHotelGeometry? Geometry { get; set; }

        [JsonProperty("opening_hours")]
        public GoogleHotelOpeningHours? OpeningHours { get; set; }

        [JsonProperty("photos")]
        public List<GoogleHotelPhoto>? Photos { get; set; }

        [JsonProperty("types")]
        public string[]? Types { get; set; }
    }

    public class GoogleHotelGeometry
    {
        [JsonProperty("location")]
        public GoogleHotelLocation? Location { get; set; }
    }

    public class GoogleHotelLocation
    {
        [JsonProperty("lat")]
        public double? Lat { get; set; }

        [JsonProperty("lng")]
        public double? Lng { get; set; }
    }

    public class GoogleHotelOpeningHours
    {
        [JsonProperty("open_now")]
        public bool? OpenNow { get; set; }
    }

    public class GoogleHotelPhoto
    {
        [JsonProperty("photo_reference")]
        public string? PhotoReference { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}