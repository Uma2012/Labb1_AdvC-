using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    public class OrderController : Controller
    {     

        [HttpPost]
        public IActionResult CreateOrder([Bind("TotalPrice,productlist")] ShoppingCart form)
        {
            Guid oriderid = Guid.NewGuid();
            int totalitems=0;
            decimal totalprice = form.TotalPrice;
            var productlist = form.productlist;
            foreach(var item in productlist)
            {
               totalitems+= item.Amount;
            }
            Order order = new Order()
            {
                OrderId = oriderid,
                OrderDate = DateTime.Now,
                TotalItems=totalitems,
                TotalPrice=totalprice
            };
            return View(order);
        }
    }
}