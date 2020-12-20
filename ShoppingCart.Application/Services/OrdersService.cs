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
        public void AddOrder(OrderViewModel orderViewModel)
        {
            Order o = new Order();
            o.DatePlaced = DateTime.UtcNow;
            o.Email = orderViewModel.Email;
            o.OrderStatus.Id = orderViewModel.OrderStatus.Id;
            o.User.UserId = orderViewModel.User.UserId;
        }

    }
}
