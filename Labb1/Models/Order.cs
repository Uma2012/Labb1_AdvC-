using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> ProductsList { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
    }

}
