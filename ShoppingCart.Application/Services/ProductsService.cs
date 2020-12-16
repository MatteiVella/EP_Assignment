using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;

        public ProductsService(IProductsRepository products)
        {
            _productRepo = products;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            var list = from p in _productRepo.GetProducts()
                       select new ProductViewModel()
                       {
                           Id = p.Id,
                           name = p.Name,
                           Description = p.Description,
                           ImageUrl = p.ImageUrl,
                           price = p.Price

                       };
            return list;
        }

        public ProductViewModel GetProduct(Guid id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            var productFromDb = _productRepo.GetProduct(id);

            productViewModel.Description = productFromDb.Description;
            productViewModel.Id = productFromDb.Id;
            productViewModel.ImageUrl = productFromDb.ImageUrl;
            productViewModel.name = productFromDb.Name;
            productViewModel.price = productFromDb.Price;
            productViewModel.Category = new CategoryViewModel();
            productViewModel.Category.Id = productFromDb.Category.Id;
            productViewModel.Category.Name = productFromDb.Category.Name;

            return productViewModel;
        }
    }
}
