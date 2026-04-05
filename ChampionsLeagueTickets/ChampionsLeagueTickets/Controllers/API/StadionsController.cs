using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadionsController : Controller
    {
        private readonly IMapper _mapper;
        private IService<Stadion> _stadionService;
        private IService<VakType> _vakTypeService;
        private IZitplaatsenService _zitplaatsenService;

        public StadionsController(IMapper mapper, IService<Stadion> stadionService, IService<VakType> vakTypeService, IZitplaatsenService zitplaatsenService)
        {
            _mapper = mapper;
            _stadionService = stadionService;
            _vakTypeService = vakTypeService;
            _zitplaatsenService = zitplaatsenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<StadionInformatieVM>>> Get()
        {
            try
            {
                var listStadions = await _stadionService.GetAllAsync();
                var listVakTypes = await _vakTypeService.GetAllAsync();

                var data = new List<StadionInformatieVM>();

                foreach (var stadion in listStadions)
                {
                    var vaktypeInfoList = new List<VakTypeInformatieVM>();

                    foreach (var vak in listVakTypes)
                    {
                        var capaciteit = await _zitplaatsenService
                            .GetCountZitplaatsenByVakTypeAndStadion(
                                stadion,
                                vak
                            );

                        vaktypeInfoList.Add(new VakTypeInformatieVM
                        {
                            VakType = _mapper.Map<VakTypeVM>(vak),
                            AantalZitplaatsen = capaciteit
                        });
                    }

                    data.Add(new StadionInformatieVM
                    {
                        Stadion = _mapper.Map<StadionVM>(stadion),
                        VakTypes = vaktypeInfoList
                    });
                }

                if (!data.Any())
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {
                    error = ex.Message 
                });
            }
        }
    }
}
