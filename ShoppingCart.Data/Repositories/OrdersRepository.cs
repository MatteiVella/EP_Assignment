using ShoppingCart.Data.Context;
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

        public void AddGuestOrder(Guid guestOrderId)
        {
            var CheckIfGuestOrderExists = _context.Order.SingleOrDefault(x => x.Id == guestOrderId);
            if (CheckIfGuestOrderExists == null)
            {

                Order o = new Order();
                o.Id = guestOrderId;
                o.DatePlaced = DateTime.MinValue;
                o.Email = null;
                o.OrderTotalPrice = 0;
                o.UserId = Guid.Empty;
                o.OrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Not_Checked_Out").Id;


                _context.Add(o);
                _context.SaveChanges();
            }
        }

        public void AddOrder(Guid userId)
        {
            Guid openOrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Not_Checked_Out").Id;
            var listOfOrdersForUserId = _context.Order.SingleOrDefault(x => x.UserId == userId && x.OrderStatusId == openOrderStatusId);

            //Checking if the user using the website has an order assosciated to their account, if not, an order will be created. If the user has an account
            // Nothing will be done.
            if (listOfOrdersForUserId == null)
            {
                Order o = new Order();
                o.DatePlaced = DateTime.MinValue;
                o.Email = _context.Members.SingleOrDefault(x => x.UserId == userId).Email;
                o.OrderTotalPrice = 0;
                o.UserId = userId;
                o.OrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Not_Checked_Out").Id;

                _context.Add(o);
            }


            _context.SaveChanges();
        }

        public void CloseOrder(Guid orderId,Guid userId)
        {
            var myOrder = _context.Order.SingleOrDefault(x => x.Id == orderId);
            var email = _context.Members.SingleOrDefault(x => x.UserId == userId).Email;

            myOrder.OrderStatusId = _context.OrderStatus.SingleOrDefault(x => x.Status == "Checked_Out").Id;
            myOrder.DatePlaced = DateTime.Now;
            myOrder.UserId = userId;
            myOrder.Email = email;

            _context.Update(myOrder);
            _context.SaveChanges();
        }
    }
}
