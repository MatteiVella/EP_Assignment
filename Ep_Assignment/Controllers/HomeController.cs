using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ep_Assignment.Models;
using ShoppingCart.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;

namespace Ep_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IOrdersService _ordersService;
        public HomeController(ILogger<HomeController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        public IActionResult Index()
        {
            //Checking if the user is a guest or not. If hes not a guest, he will create a temp OrderId and store it in the cookies
            //If the user is authenticated, an OrderId will be created, only if the user doesnt have an order which is currently open
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Request.Cookies["tempOrder_id"] == null)
                {
                    var orderId = Guid.NewGuid();
                    string test = Convert.ToString(orderId);
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("tempOrder_id", test, cookie);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Guid GuidUserId = Guid.Empty;
                Guid.TryParse(userId, out GuidUserId);
                _ordersService.AddOrder(GuidUserId);
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, code = code});
        }
    }
}
