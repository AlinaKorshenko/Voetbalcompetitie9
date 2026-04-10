using Newtonsoft.Json;

namespace ChampionsLeagueTickets.ViewModels
{
    public class HotelVM
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Address { get; set; }

        public double? Rating { get; set; }

        public string[]? Amenities { get; set; }
    }
}
