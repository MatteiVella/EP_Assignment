using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRep;
        public OrdersService(IOrdersRepository ordersRep)
        {
            _ordersRep = ordersRep;
        }
        public void AddOrder(Guid userId)
        {
            _ordersRep.AddOrder(userId);
        }

        public void CloseOrder(Guid orderId)
        {
            _ordersRep.CloseOrder(orderId);
        }

    }
}
