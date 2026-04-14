using ChampionsLeagueTickets.Extentions;
using ChampionsLeagueTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTickets.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartKey = "ShoppingCart";

        public IActionResult Index()
        {
            var cart = GetShoppingCart();
            return View(cart);
        }

        public IActionResult AddToCart(ShoppingCartVM vm)
        {
            return RedirectToAction("Index");
        }

        private ShoppingCartVM? GetShoppingCart()
        {
            var vm = HttpContext.Session.GetObject<ShoppingCartVM>(CartKey);
            return vm;
        }

        private void SaveShoppingCart(ShoppingCartVM cart)
        {
            HttpContext.Session.SetObject(CartKey, cart);
        }

    }
}
