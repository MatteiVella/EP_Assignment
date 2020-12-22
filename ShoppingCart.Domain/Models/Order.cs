using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime DatePlaced { get; set; }
        
        public double OrderTotalPrice { get; set; }

        [ForeignKey("Member")]
        public Guid UserId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("OrderStatus")]
        public Guid OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }

}
