﻿using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private ShoppingCartDbContext _context;
        public OrdersRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void AddOrder(Guid userId)
        {
            Order o = new Order();
            o.DatePlaced = DateTime.MinValue;
            o.Email = _context.Members.SingleOrDefault(x => x.UserId == userId).Email;
            o.OrderTotalPrice = 0;
            o.UserId = userId;
            o.OrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Not_Checked_Out").Id;

            _context.Add(o);
            _context.SaveChanges();
        }

        public void CloseOrder(Guid orderId)
        {
            var myOrder = _context.Order.SingleOrDefault(x => x.Id == orderId);
            myOrder.OrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Checked_Out").Id;
            myOrder.DatePlaced = DateTime.Now;

            _context.Update(myOrder);
            _context.SaveChanges();
        }
    }
}
