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
        private IProductsService _productsService;

        public OrderController(IOrdersDetailsService ordersDetailsService, IOrdersService ordersService, IProductsService productsService)
        {
            _ordersService = ordersService;
            _ordersDetailsService = ordersDetailsService;
            _productsService = productsService;
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
            _ordersDetailsService.SetTotal(orderId);
            ViewBag.OrderTotalPrice = totalPrice;
            ViewBag.OrderId = orderId;
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
            var stockForProductId = _productsService.GetProduct(productId).Stock;

            if(stockForProductId == 0)
            {
                TempData["warning"] = "Unfortunatley, The Chosen Product is Out Of Stock.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(userId, out GuidUserId);

            _ordersDetailsService.AddToCart(productId,GuidUserId);
            var orderId = _ordersDetailsService.GetOrderId(GuidUserId);
            _ordersDetailsService.SetTotal(orderId);
            TempData["success"] = "Product Was Added to the Cart Succesfully";
            return RedirectToAction("Details","Products", new { id = productId });

        }

        public IActionResult DeleteFromOrderDetails(Guid productId, Guid orderId)
        {
            _ordersDetailsService.DeleteFromOrderDetails(productId,orderId);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Checkout");
        }

        public IActionResult PurchaseOrder(Guid orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(userId, out GuidUserId);

            try
            {
                _ordersService.CloseOrder(orderId);
                _ordersService.AddOrder(GuidUserId);
                TempData["Success"] = "Your Order Has been Successfully Checked Out.";
            }catch(Exception ex)
            {
                TempData["Warning"] = ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
