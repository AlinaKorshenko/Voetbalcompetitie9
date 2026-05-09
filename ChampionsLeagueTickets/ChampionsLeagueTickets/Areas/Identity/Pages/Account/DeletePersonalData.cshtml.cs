#nullable disable
using System.ComponentModel.DataAnnotations;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChampionsLeagueTickets.Areas.Identity.Pages.Account
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IOrderService _orderService;
        private readonly IOrderLijnenService _orderLijnService;
        private readonly ITicketService _ticketService;
        private readonly IAbonnementService _abonnementService;

        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IOrderService orderService,
            IOrderLijnenService orderLijnService,
            ITicketService ticketService,
            IAbonnementService abonnementService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _orderService = orderService;
            _orderLijnService = orderLijnService;
            _ticketService = ticketService;
            _abonnementService = abonnementService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Wachtwoord is verplicht in te vullen.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (Input?.Password == null || !await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Het wachtwoord is niet juist.");
                    return Page();
                }
            }

            var userId = user.Id;

            var orders = await _orderService.GetAllByUserId(userId);

            foreach (var order in orders ?? [])
            {
                var orderlijnen = order.Orderlijnens.ToList();

                foreach (var orderLijn in orderlijnen)
                {
                    var fresh = await _orderLijnService.FindByOrderIdAndOrderLijnNumber(
                        orderLijn.OrderId, orderLijn.OrderLijnNummer);

                    var ticket = fresh?.Ticket;
                    var abonnement = fresh?.Abonnementen;

                    // 1. Delete orderlijn first
                    await _orderLijnService.DeleteAsync(fresh);

                    //ticket en abonnement verwijderen
                    if (ticket != null)
                    {
                        await _ticketService.DeleteAsync(ticket);
                    }

                    if (abonnement != null)
                    {
                        await _abonnementService.DeleteAsync(abonnement);
                    }
                }

                //order verwijderen
                await _orderService.DeleteAsync(order);
            }

            //user in identity verwijderen
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Unexpected error occurred deleting user.");
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
