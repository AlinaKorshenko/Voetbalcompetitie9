using AutoMapper;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.ViewModels.order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;

namespace ChampionsLeagueTickets.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IOrderLijnenService _orderLijnService;
        private readonly ITicketService _ticketService;
        private readonly IAbonnementService _abonementService;

        public OrderController(UserManager<IdentityUser> userManager, IOrderService orderService, IMapper mapper, IOrderLijnenService orderLijnService, ITicketService ticketService, IAbonnementService abonnementService)
        {
            _userManager = userManager;
            _orderService = orderService;
            _mapper = mapper;
            _orderLijnService = orderLijnService;
            _ticketService = ticketService;
            _abonementService = abonnementService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var orders = await _orderService.GetAllByUserId(userId);

            var ordersVM = _mapper.Map<List<OrderVM>>(orders);

            return View(ordersVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string orderId, int orderLijnNummer) {
            var orderLijn = await _orderLijnService.FindByOrderIdAndOrderLijnNumber(orderId, orderLijnNummer);

            if (orderLijn == null)
                return NotFound();

            var ticket = orderLijn.Ticket;
            var abonnement = orderLijn.Abonnementen;

            await _orderLijnService.DeleteAsync(orderLijn);

            var order = await _orderService.FindByIdAsync(orderId);

            if (order.Orderlijnens.Count == 0)
            {
                await _orderService.DeleteAsync(order);
            }

            if (abonnement != null)
            {
                await _abonementService.DeleteAsync(abonnement);
            }

            if (ticket != null)
            {
                await _ticketService.DeleteAsync(ticket);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
