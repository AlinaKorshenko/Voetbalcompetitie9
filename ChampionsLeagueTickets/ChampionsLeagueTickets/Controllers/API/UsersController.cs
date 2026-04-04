using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

    }
}
