using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersDetailsRepository
    {
        public void AddToCart(OrderDetails orderDetails);
        public void Dispose();
        public Guid GetOrderId(Guid UserId);
        public IQueryable<OrderDetails> GetOrderItems(Guid orderId);

    }
}
