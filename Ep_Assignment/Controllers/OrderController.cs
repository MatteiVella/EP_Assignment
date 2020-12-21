using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ep_Assignment.Controllers
{
    public class OrderController : Controller
    {
        private IOrdersDetailsService _ordersDetailsService;
        private IOrdersService _ordersService;

        public OrderController(IOrdersDetailsService ordersDetailsService, IOrdersService ordersService)
        {
            _ordersService = ordersService;
            _ordersDetailsService = ordersDetailsService;
        }

        public IActionResult Checkout(Guid orderId)
        {
            var listOfOrderItems = _ordersDetailsService.GetOrderItems(orderId);
            return View(listOfOrderItems);

        }
    }
}
