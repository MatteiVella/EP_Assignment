using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        public void AddOrder(Guid userId);
        public void AddGuestOrder(Guid guestOrderId);
        public void CloseOrder(Guid orderId);
    }
}
