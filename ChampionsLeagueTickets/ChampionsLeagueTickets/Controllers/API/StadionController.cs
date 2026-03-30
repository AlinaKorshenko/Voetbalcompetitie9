using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadionController : Controller
    {
        private IService<Stadion> _stadionService;
        private readonly IMapper _mapper;
        public StadionController(IMapper mapper, IService<Stadion> stadionService)
        {
            _mapper = mapper;
            _stadionService = stadionService;
        }

        [HttpGet]
        public async Task<ActionResult<StadionVM>> Get()
        {
            try
            {
                var list = await _stadionService.GetAllAsync();
                List<StadionVM> data = _mapper.Map<List<StadionVM>>(list);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }
    }
}
