using ChampionsLeagueTickets.Domain.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class UserService : IUserInterface
    {
        UserManager _userManager;

        public UserService(UserManager userManager)
        {
            this._userManager = userManager;
        }

        public Task<IEnumerable<UserInfoResponse?>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();

            return users.Select(u => new UserInfoResponse(
                u.Email!
            ));
        }
    }
}
