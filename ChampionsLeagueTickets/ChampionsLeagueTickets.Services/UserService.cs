using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.DTO;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class UserService : IUserService
    {
        private readonly FootballDbContext _context;

        public UserService(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserInfoResponse>> GetAllUsersAsync()
        {
            return await _context.AspNetUsers
                .Select(u => new UserInfoResponse(
                    u.Email!
                ))
                .ToListAsync();
        }
    }
}
