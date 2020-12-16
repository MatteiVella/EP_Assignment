using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagerUrl { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
    }
}
