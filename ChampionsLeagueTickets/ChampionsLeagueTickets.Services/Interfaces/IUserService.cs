using ChampionsLeagueTickets.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoResponse?>> GetAllUsersAsync();
    }
}
