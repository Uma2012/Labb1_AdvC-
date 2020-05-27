using OrderService.Data;
using OrderService.Models;
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

       

         public  Order CreateOrder(Order order)
        {
             _context.Orders.Add(order);
            _context.SaveChanges();
            return order;

        }

        public bool Delete(Guid id)
        {
            try
            {
                var order = GetOrderById(id);
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public Order GetOrderById(Guid orderid)
        {
            //List<Order> orderlist = new List<Order>();
            //int orderrows=  _context.Orders.Count(x => x.OrderId == orderid);
            //Order order = new Order();
            //for (int i = 0; i < orderrows; i++)
            //{
            //     order = _context.Orders.FirstOrDefault(x => x.OrderId == orderid);
            //    orderlist[i] = order;
            //}
            //return order;


            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderid);
            return order;
        }

       
    }
}
