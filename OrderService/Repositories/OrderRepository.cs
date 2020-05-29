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
        //Creates order to database
        public  Order CreateOrder(Order order)
         {
            if (order.OrderId == null || order.OrderDate == null || order.ProductId == Guid.Empty)
                return order = null;
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       //Delete an order
        public bool Delete(Guid id)
        {
            try
            {
               var order = GetOrderById(id);
                if (order != null)
                {
                    _context.Orders.RemoveRange(order);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }           
        }

        //Get order by its id
        public List<Order> GetOrderById(Guid orderid)
        {
            var IsContains = _context.Orders.FirstOrDefault(x=>x.OrderId==orderid);
            if (IsContains != null)
            {
                List<Order> order = new List<Order>();
                order = _context.Orders.Where(x => x.OrderId == orderid).ToList();
                return order;
            }
            else
                return null;           
        }
       
    }
}
