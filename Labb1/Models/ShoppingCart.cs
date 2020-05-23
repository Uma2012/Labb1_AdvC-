using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class ShoppingCart
    {       
        public List<CartItem> productlist { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid  Userid { get; set; }
    }

    public class CartItem
    {
        public int Amount { get; set; }
        public Products Product { get; set; }
    }
}
