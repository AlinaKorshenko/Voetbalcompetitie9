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
        private IService<VakType> _vakTypeService;
        private readonly IMapper _mapper;
        public StadionController(IMapper mapper, IService<Stadion> stadionService, IService<VakType> vakTypeService)
        {
            _mapper = mapper;
            _stadionService = stadionService;
            _vakTypeService = vakTypeService;
        }

        //[HttpGet]
        //public async Task<ActionResult<StadionVM>> Get()
        //{
        //    try
        //    {
        //        var listStadions = await _stadionService.GetAllAsync();
        //        List<StadionVM> stadions = _mapper.Map<List<StadionVM>>(listStadions);
        //        if (stadions == null)
        //        {
        //            return NotFound();
        //        }

        //        var listVakTypes = await _vakTypeService.GetAllAsync();
        //        List<StadionVM> vakTypes = _mapper.Map<List<StadionVM>>(listVakTypes);
        //        if (stadions == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            error = ex.Message
        //        });
        //    }
        //}
    }
}
