using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class Order
    {
        public int id { get; set; }
        public Guid OrderId { get; set; }
     
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId   { get; set; }
    }
}
