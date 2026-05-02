using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ChampionsLeagueTickets.ViewModels
{
    public class HotelVM
    {
        public SelectList? Stadiums { get; set; }
        public string? SelectedStadiumId { get; set; }
        public string? SelectedStadiumName { get; set; }
        public List<GoogleHotelItem>? Hotels { get; set; }
    }
}
