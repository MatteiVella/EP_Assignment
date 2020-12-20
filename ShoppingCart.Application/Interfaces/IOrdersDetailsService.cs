using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrdersDetailsService
    {
        public void AddToCart(OrderDetailsViewModel orderDetailsViewModel);
        public void Dispose();
        public Guid GetOrderId(Guid userId);
        public IQueryable<OrderDetailsViewModel> GetOrderItems(Guid orderId);
    }
}
