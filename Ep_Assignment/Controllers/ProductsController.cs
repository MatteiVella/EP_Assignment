using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System.IO;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace Ep_Assignment.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoriesService _categoriesService;
        private IWebHostEnvironment _environment;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService,IWebHostEnvironment environment)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index( int? page)
        {
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            var list = _productsService.GetProducts();
            var pageNumber = page ?? 1;
            var pageSize = 10;

            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public IActionResult Index(int? page,string categoryName)
        {

            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;
            var list = _productsService.GetProductsByCategory(categoryName);
         
            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Details(Guid id)
        {
            var product = _productsService.GetProduct(id);
            return View(product);
        }

        [HttpGet] //Loads the field with blanks
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;
            return View();
        }

        [HttpPost]//Called when the user clicks button
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile file)
        {
            try
            {
                if(file != null)
                {
                    if(file.Length > 0)
                    {
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        string absolutePath = _environment.WebRootPath + @"\Images\";

                        using (FileStream stream = System.IO.File.Create(absolutePath + newFileName))
                        {
                            file.CopyTo(stream);
                        }
                        data.ImageUrl = @"\Images\" + newFileName;
                    }
                }
                _productsService.AddProduct(data);

                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                ViewData["Warning"] = ex.Message;
            }

            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public IActionResult Hide(Guid id)
        {
            _productsService.HideProduct(id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
