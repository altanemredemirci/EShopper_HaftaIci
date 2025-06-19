using BLL.Abstract;
using CORE.Entities;
using CORE.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.Controllers
{
    [Authorize] //Oturum Açık mı?
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _cartService.GetOneAsync(i => i.ApplicationUserId == userId);
            return View(cart);
        }
      
        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            var userId = _userManager.GetUserId(User); 
            var cart = await _cartService.GetOneAsync(i => i.ApplicationUserId == userId);
            if (cart == null)
            {
                cart = new Cart() { ApplicationUserId = userId };

                await _cartService.CreateAsync(cart);
            }

            await _cartService.AddToCartAsync(cart, id, quantity);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFromCart(int productId, int cartId)
        {
            await _cartService.DeleteFromCartAsync(cartId, productId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _cartService.GetOneAsync(i => i.ApplicationUserId == userId);
            return View(cart);
        }
    }
}
