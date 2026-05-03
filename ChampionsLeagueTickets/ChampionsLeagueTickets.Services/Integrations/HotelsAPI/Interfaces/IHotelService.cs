using ChampionsLeagueTickets.Domain.Integrations.HotelsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IHotelService
    {
        Task<GoogleHotelApiDTO?> GetNearbyHotelsAsync(double lat, double lng, int radiusMeters = 5000);
    }
}
