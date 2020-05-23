using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        //public Order CreateOrder(ShoppingCart shoppingCart);
        public Order CreateOrder(Order order);

        public bool Delete(Guid id);
        public Order GetOrderById(Guid orderid);
    }
}
