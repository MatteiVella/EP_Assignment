using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
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
        public Guid ShoppingCartId { get; set; }
        private IOrdersDetailsRepository _orderRepo;
        private IProductsRepository _productRepo;
        private IMembersRepository _memberRepo;
        public OrdersDetailsService(IOrdersDetailsRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public void AddToCart(OrderDetailsViewModel odvm)
        {
            ShoppingCartId = GetOrderId(odvm.Order.User.UserId);
            var productFromDb = _productRepo.GetProduct(odvm.Product.Id);



            OrderDetails od = new OrderDetails();
            od.OrderId = ShoppingCartId;
            od.ProductId = productFromDb.Id;
            od.Quantity = 1;
            od.SoldPrice = productFromDb.Price;

            _orderRepo.AddToCart(od);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Guid GetOrderId(Guid UserId)
        {
            return _orderRepo.GetOrderId(UserId);
        }

        public IQueryable<OrderDetailsViewModel> GetOrderItems(Guid orderId)
        {

            var list = from p in _orderRepo.GetOrderItems(orderId)
                       select new OrderDetailsViewModel()
                       {
                           Id = p.Id,
                           Product = new ProductViewModel() { Id = p.Product.Id },
                           Order = new OrderViewModel() { Id = p.Order.Id},
                           Quantity = p.Quantity,
                           SoldPrice = p.SoldPrice

                       };
            return list;
        }
    }
}
