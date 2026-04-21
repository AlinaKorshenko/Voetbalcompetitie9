using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.DTO
{
    public record UserInfoResponse(
        string UserName,
        string Email,
        string Role
    );
}
