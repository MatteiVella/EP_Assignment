using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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
            if (HttpContext.User.Identity.IsAuthenticated)
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
                ViewBag.Count = 0;
                return View(listOfOrderItems);
            }
            else
            {
                var orderId = HttpContext.Request.Cookies["tempOrder_id"];
                Guid GuidOrderId = Guid.Empty;
                Guid.TryParse(orderId, out GuidOrderId);
                var listOfOrderItems = _ordersDetailsService.GetOrderItems(GuidOrderId);
                var totalPrice = _ordersDetailsService.GetTotal(GuidOrderId);
                ViewBag.OrderTotalPrice = totalPrice;
                ViewBag.OrderId = GuidOrderId;
                return View(listOfOrderItems);
            }

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
                TempData["Warning"] = "Unfortunatley, The Chosen Product is Out Of Stock.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(userId, out GuidUserId);

            if (userId == null)
            {
                var orderId = HttpContext.Request.Cookies["tempOrder_id"];
                Guid GuidOrderId = Guid.Empty;
                Guid.TryParse(orderId, out GuidOrderId);

                _ordersService.AddGuestOrder(GuidOrderId);
                _ordersDetailsService.AddToGuestCart(productId, GuidOrderId);
                _ordersDetailsService.SetTotal(GuidOrderId);

            }
            else
            {
                _ordersDetailsService.AddToCart(productId, GuidUserId);
                var orderId = _ordersDetailsService.GetOrderId(GuidUserId);
                _ordersDetailsService.SetTotal(orderId);
            }


            TempData["Success"] = "Product Was Added to the Cart Succesfully";
            return RedirectToAction("Details","Products", new { id = productId });

        }

        public IActionResult DeleteFromOrderDetails(Guid productId, Guid orderId)
        {
            _ordersDetailsService.DeleteFromOrderDetails(productId,orderId);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Checkout");
        }

        [Authorize]
        public IActionResult PurchaseOrder(Guid orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid GuidUserId = Guid.Empty;
            Guid.TryParse(userId, out GuidUserId);

            var cookieOrderId = HttpContext.Request.Cookies["tempOrder_id"];
            Guid GuidCookieOrderId = Guid.Empty;
            Guid.TryParse(cookieOrderId, out GuidCookieOrderId);


            try
            {
                _ordersService.CloseOrder(orderId,GuidUserId);
                _ordersService.AddOrder(GuidUserId);

                if (orderId == GuidCookieOrderId)
                {
                    Response.Cookies.Delete("tempOrder_id");
                }

                TempData["Success"] = "Your Order Has been Successfully Checked Out.";

            }
            catch (Exception ex)
            {
                TempData["Warning"] = ex.Message;
            }

            return RedirectToAction("Index", "Home");
           
        }
    }
}
