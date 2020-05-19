using OrderService.Data;
using OrderService.Models;
using OrderService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            this._context = context;
        }

        public Order CreateOrder(ShoppingCart shoppingCart)
        {
            OrderViewModel vm = new OrderViewModel();
            Guid oriderid = Guid.NewGuid();
            int totalitems = 0;
            decimal totalprice = shoppingCart.TotalPrice;
            var productlist = shoppingCart.productlist;
            foreach (var item in productlist)
            {
                totalitems += item.Amount;
            }
            Order order = new Order()
            {
                OrderId = oriderid,
                OrderDate = DateTime.Now,
                TotalItems = totalitems,
                TotalPrice = totalprice,
                //UserId = Guid.Parse(_userManager.GetUserId(User)),
                ProductsList = shoppingCart.productlist

            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }
    }
}
