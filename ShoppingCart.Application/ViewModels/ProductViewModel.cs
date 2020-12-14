using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
