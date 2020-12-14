using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.Interfaces;

namespace Ep_Assignment.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        public IActionResult Index()
        {
            var list = _productsService.GetProducts();

            return View(list);
        }
    }
}
