using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Services.DTO;
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

        async Task<IEnumerable<UserInfoResponse?>> IUserService.GetAllUsersAsync()
        {
            try
            {
                return await _context.AspNetUsers
                    .Select(u => new UserInfoResponse(
                        u.UserName,
                        u.Email,
                        u.Roles.Select(r => r.Name).FirstOrDefault() ?? "No Role"
                    ))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in servicelaag: " + ex.Message);
                throw;
            }
        }
    }
}
