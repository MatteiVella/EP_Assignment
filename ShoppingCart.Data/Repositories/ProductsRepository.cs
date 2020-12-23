using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {

        private ShoppingCartDbContext _context;

        public ProductsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void AddProduct(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void HideProduct(Guid id)
        {
            var myProduct = GetProduct(id);
            myProduct.isVisible = false;
            _context.Update(myProduct);
            _context.SaveChanges();
        }

        public Product GetProduct(Guid id)
        {
            return _context.Products.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products.Where(x => x.isVisible == true);
        }

        public IQueryable<Product> GetProductsByCategory(string categoryName)
        {
            if(categoryName == null)
            {
                return _context.Products.Where(x => x.isVisible == true);
            }
            return _context.Products.Where(x => x.isVisible == true && x.Category.Name == categoryName);
        }
    }
}
