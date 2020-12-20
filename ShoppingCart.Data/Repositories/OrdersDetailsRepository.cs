using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;

using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace ShoppingCart.Data.Repositories
{
    public class OrdersDetailsRepository : IOrdersDetailsRepository
    {
        private ShoppingCartDbContext _context;
        public OrdersDetailsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void AddToCart(OrderDetails orderDetails)
        {
            var orderItem = _context.OrderDetails.SingleOrDefault(
                x => x.OrderId == orderDetails.OrderId && x.ProductId == orderDetails.ProductId);

            if(orderItem == null)
            {
                _context.OrderDetails.Add(orderDetails);
            }
            else
            {
                orderItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        public Guid GetOrderId(Guid userId)
        {
            return _context.Order
                .Where(x => x.OrderStatus.Status == "Not_Checked_Out")
                .SingleOrDefault(x => x.User.UserId == userId).Id;
        }

        public IQueryable<OrderDetails> GetOrderItems(Guid orderId)
        {
            return _context.OrderDetails.Where(x => x.Id == orderId);
        }
    }
}
