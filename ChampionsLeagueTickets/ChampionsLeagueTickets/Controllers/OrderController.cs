using AutoMapper;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels.order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace ChampionsLeagueTickets.Controllers
{
    public class OrderController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public OrderController(UserManager<IdentityUser> userManager, IOrderService orderService, IMapper mapper)
        {
            _userManager = userManager;
            _orderService = orderService;
            _mapper = mapper;

        }


        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var orders = await _orderService.GetAllByUserId(userId);

            var ordersVM = _mapper.Map<List<OrderVM>>(orders);



            return View(ordersVM);
        }
    }
}
