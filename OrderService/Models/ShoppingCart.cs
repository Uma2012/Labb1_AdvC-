using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class ShoppingCart
    {
        public List<CartItem> productlist { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
