using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrdersDetailsService
    {
        public void AddToCart(Guid productId, Guid userId);
        public void Dispose();
        public Guid GetOrderId(Guid userId);
        public IQueryable<OrderDetailsViewModel> GetOrderItems(Guid orderId);
        public OrderDetailsViewModel GetOneOrderDetail(Guid orderId, Guid productId);
        public double GetTotal(Guid orderId);
        public Guid GetStatusId(string StatusName);
        public void DeleteFromOrderDetails(Guid productId, Guid orderId);
    }
}
