using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class CartItem
    {
        public int Amount { get; set; }
        public Products Product { get; set; }
    }
}
