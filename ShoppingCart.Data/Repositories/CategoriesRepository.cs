using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    class CategoriesRepository : ICategoriesRepository
    {
        public IQueryable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
