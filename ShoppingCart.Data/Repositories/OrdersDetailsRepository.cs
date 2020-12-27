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

        public void AddToCart(Guid productId, Guid userId)
        {
            var orderId = GetOrderId(userId);
            var orderItem = _context.OrderDetails.SingleOrDefault(
                x => x.OrderId == orderId && x.ProductId == productId);
            var product = _context.Products.SingleOrDefault(
                x => x.Id == productId);

            if(orderItem == null)
            {
                _context.OrderDetails.Add(new OrderDetails() { OrderId = orderId, ProductId = productId,Quantity = 1,SoldPrice = product.Price  }) ;
            }
            else
            {
                orderItem.Quantity++;
            }
            product.Stock--;
            _context.Update(product);
            _context.SaveChanges();
        }
        
        public void AddToGuestCart(Guid productId, Guid orderId)
        {
            var orderItem = _context.OrderDetails.SingleOrDefault(
                x => x.OrderId == orderId && x.ProductId == productId);
            var product = _context.Products.SingleOrDefault(
                x => x.Id == productId);

            if (orderItem == null)
            {
                _context.OrderDetails.Add(new OrderDetails() { OrderId = orderId, ProductId = productId, Quantity = 1, SoldPrice = product.Price });
            }
            else
            {
                orderItem.Quantity++;
            }
            product.Stock--;
            _context.Update(product);
            _context.SaveChanges();
        }

        public Guid GetOrderId(Guid userId)
        {
            var Order = _context.Order
                .Where(x => x.OrderStatus.Status == "Not_Checked_Out")
                .SingleOrDefault(x => x.UserId == userId);
            Guid orderId = Order.Id;
            return orderId;
        }

        public Guid GetStatusId(string StatusName)
        {

            Guid status = _context.OrderStatus.SingleOrDefault(x => x.Status == StatusName).Id;
            return status;
        }

        public IQueryable<OrderDetails> GetOrderItems(Guid orderId)
        {
            return _context.OrderDetails.Where(x => x.OrderId == orderId && x.Product.isVisible == true);
        }

        public double GetTotal(Guid orderId)
        {
            double total = 0;
            foreach (var i in _context.OrderDetails.Where(x => x.OrderId == orderId && x.Product.isVisible == true))
            {
                double totalOfOneProduct = i.Quantity * i.Product.Price;
                total = total + totalOfOneProduct;
            }
            return total;
            
        }

        public void SetTotal(Guid orderId)
        {
            double total = 0;
            foreach (var i in _context.OrderDetails.Where(x => x.OrderId == orderId && x.Product.isVisible == true))
            {
                double totalOfOneProduct = i.Quantity * i.Product.Price;
                total = total + totalOfOneProduct;
            }
            var Order = _context.Order.SingleOrDefault(x => x.Id == orderId);
            Order.OrderTotalPrice = total;
            _context.Update(Order);
            _context.SaveChanges();
        }

        public OrderDetails GetOneOrderDetail(Guid orderId, Guid productId)
        {
            return _context.OrderDetails.Where(x => x.ProductId == productId).SingleOrDefault(x => x.OrderId == orderId);
        }

        public void DeleteFromOrderDetails(Guid productId, Guid orderId)
        {
            var orderDetail = GetOneOrderDetail(orderId, productId);
            int quantity = orderDetail.Quantity;
            _context.OrderDetails.Remove(orderDetail);

            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            product.Stock = product.Stock + quantity;
            _context.Update(product);

            _context.SaveChanges();
        }

    }
}
