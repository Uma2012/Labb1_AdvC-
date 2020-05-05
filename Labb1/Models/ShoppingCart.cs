using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class ShoppingCart
    {
        //public Guid CartId { get; set; }
        //public Guid ProductId { get; set; }
        //public Products Product { get; set; }
        public List<CartItem> productlist { get; set; }
        public decimal TotalPrice { get; set; }
        // public int Amount { get; set; }
    }

    public class CartItem
    {
        public int Amount { get; set; }
        public Products Product { get; set; }
    }
}
