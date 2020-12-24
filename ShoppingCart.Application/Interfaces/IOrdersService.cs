using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrdersService
    {
        public void AddOrder(Guid userId);
        public void CloseOrder(Guid orderId);
    }
}
