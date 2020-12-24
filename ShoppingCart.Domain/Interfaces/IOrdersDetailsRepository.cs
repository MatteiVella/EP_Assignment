using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersDetailsRepository
    {
        public void AddToCart(Guid productId,Guid orderId);
        public void Dispose();
        public Guid GetOrderId(Guid UserId);
        public IQueryable<OrderDetails> GetOrderItems(Guid orderId);
        public OrderDetails GetOneOrderDetail(Guid orderId, Guid productId);
        public double GetTotal(Guid orderId);
        public void SetTotal(Guid orderId);
        public Guid GetStatusId(string StatusName);
        public void DeleteFromOrderDetails(Guid productId, Guid orderId);

    }
}
