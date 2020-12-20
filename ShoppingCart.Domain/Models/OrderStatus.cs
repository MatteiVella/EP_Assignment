using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
