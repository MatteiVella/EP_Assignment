using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    class CategoriesService : ICategoriesService
    {
        private ICategoriesService _categoriesService;
        public CategoriesService(ICategoriesService categories)
        {
            _categoriesService = categories;
        }
        public IQueryable<CategoryViewModel> GetCategories()
        {
            var list = from c in _categoriesService.GetCategories()
                       select new CategoryViewModel()
                       {
                           Id = c.Id,
                           Name = c.Name
                       };

            return list;
        }
    }
}
