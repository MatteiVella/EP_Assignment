using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Ep_Assignment.Controllers
{
    public class OrderController : Controller
    {
        private IOrdersDetailsService _ordersDetailsService;
        private IOrdersService _ordersService; 
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrdersDetailsService ordersDetailsService, IOrdersService ordersService)
        {
            _ordersService = ordersService;
            _ordersDetailsService = ordersDetailsService;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(currentUserId, out GuidUserId);
            Guid orderId = _ordersDetailsService.GetOrderId(GuidUserId);

            var listOfOrderItems = _ordersDetailsService.GetOrderItems(orderId);
            var totalPrice = _ordersDetailsService.GetTotal(orderId);
            ViewBag.OrderTotalPrice = totalPrice; 
            return View(listOfOrderItems);

        }

        public IActionResult UpdateCart(Guid orderId)
        {
            var listOfOrderItems = _ordersDetailsService.GetOrderItems(orderId);
            var totalPrice = _ordersDetailsService.GetTotal(orderId);
            ViewBag.OrderTotalPrice = totalPrice;
            return View(listOfOrderItems);
        }

        public IActionResult AddToCart(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(userId, out GuidUserId);

            _ordersDetailsService.AddToCart(productId,GuidUserId);
            return RedirectToAction("Details","Products", new { id = productId });

        }

        public IActionResult DeleteFromOrderDetails(Guid productId, Guid orderId)
        {
            _ordersDetailsService.DeleteFromOrderDetails(productId,orderId);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Checkout");
        }
    }
}
