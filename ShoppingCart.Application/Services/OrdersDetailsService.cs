using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System.Security.Claims;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrdersDetailsService : IOrdersDetailsService
    {
        public Guid orderId { get; set; }
        private IOrdersDetailsRepository _orderDetailsRepo;
        private IOrdersRepository _ordersRepo;
        public OrdersDetailsService(IOrdersDetailsRepository orderDetailsRepo, IOrdersRepository ordersRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _ordersRepo = ordersRepo;
        }
        public void AddToCart(Guid productId, Guid userId)
        {
            orderId = GetOrderId(userId);
            _orderDetailsRepo.AddToCart(productId,orderId);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Guid GetOrderId(Guid UserId)
        {

            Guid OrderId = _orderDetailsRepo.GetOrderId(UserId);
            /*if(OrderId == Guid.Empty)
            {
                Guid TempGuestGuid = Guid.NewGuid();
                _ordersRepo.AddOrder(new Order { Id = TempGuestGuid, OrderStatusId = GetStatusId("Not_Checked_Out")});
                return TempGuestGuid;
            }
            else
            {*/
                return OrderId;
            //}
        }

        public Guid GetStatusId(string StatusName)
        {

            return _orderDetailsRepo.GetStatusId(StatusName);
        }

        public IQueryable<OrderDetailsViewModel> GetOrderItems(Guid orderId)
        {

            var list = from p in _orderDetailsRepo.GetOrderItems(orderId)
                       select new OrderDetailsViewModel()
                       {
                           Id = p.Id,
                           Product = new ProductViewModel() { Id = p.Product.Id, ImageUrl= p.Product.ImageUrl,  Name = p.Product.Name, Price = p.Product.Price},
                           Order = new OrderViewModel() { Id = p.Order.Id, Email = p.Order.Email, OrderStatus = p.Order.OrderStatus, OrderTotalPrice = p.Order.OrderTotalPrice},
                           Quantity = p.Quantity,
                           SoldPrice = p.SoldPrice,
                       };
            
            return list;
        }

        public double GetTotal(Guid orderId)
        {
            return _orderDetailsRepo.GetTotal(orderId);
        }

        public void SetTotal(Guid orderId)
        {
            _orderDetailsRepo.SetTotal(orderId);
        }
        public void DeleteFromOrderDetails(Guid productId, Guid orderId)
        {
            if (_orderDetailsRepo.GetOneOrderDetail(orderId, productId) != null)
                _orderDetailsRepo.DeleteFromOrderDetails(productId, orderId);
        }

        public OrderDetailsViewModel GetOneOrderDetail(Guid orderId, Guid productId)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            var orderDetailFromDb = _orderDetailsRepo.GetOneOrderDetail(orderId, productId);

            model.Id = orderDetailFromDb.Id;
            model.Order = new OrderViewModel();
            model.Order.Id = orderDetailFromDb.Order.Id;
            model.Order.OrderStatus = orderDetailFromDb.Order.OrderStatus;
            model.Order.OrderTotalPrice = orderDetailFromDb.Order.OrderTotalPrice;
            model.Order.User.UserId = orderDetailFromDb.Order.UserId;
            model.Product.Id = orderDetailFromDb.Product.Id;
            model.Product.ImageUrl = orderDetailFromDb.Product.ImageUrl;

            return model;
            
        }
    }
}
