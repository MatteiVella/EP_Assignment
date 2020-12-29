using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System.Security.Claims;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ShoppingCart.Application.Services
{
    public class OrdersDetailsService : IOrdersDetailsService
    {
        public Guid orderId { get; set; }
        private IOrdersDetailsRepository _orderDetailsRepo;
        private IMapper _mapper;
        public OrdersDetailsService(IOrdersDetailsRepository orderDetailsRepo, IMapper mapper)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _mapper = mapper;
        }
        public void AddToCart(Guid productId, Guid userId)
        {
            _orderDetailsRepo.AddToCart(productId, userId);
        }
        public void AddToGuestCart(Guid productId, Guid orderId)
        {
            _orderDetailsRepo.AddToGuestCart(productId,orderId);
        }

        public Guid GetOrderId(Guid UserId)
        {
            return _orderDetailsRepo.GetOrderId(UserId);
        }

        public Guid GetStatusId(string StatusName)
        {

            return _orderDetailsRepo.GetStatusId(StatusName);
        }

        public IQueryable<OrderDetailsViewModel> GetOrderItems(Guid orderId)
        {
            return _orderDetailsRepo.GetOrderItems(orderId).ProjectTo<OrderDetailsViewModel>(_mapper.ConfigurationProvider);
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
            return _mapper.Map<OrderDetailsViewModel>(_orderDetailsRepo.GetOneOrderDetail(orderId, productId));          
        }
    }
}
