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
        private readonly string _cartName;
        private readonly IProductRepository _productRepository;
        public OrderController(IProductRepository productRepository, IConfiguration config)
        {
            this._cartName = config["CartSessionCookie:Name"];
            this._productRepository = productRepository;
        }
       

        [HttpPost]
        public IActionResult CreateOrder(IFormCollection form)
        {
            Guid orderid = Guid.NewGuid();
            var cart = HttpContext.Session.Get<List<CartItem>>(_cartName);
            decimal totalprice = Convert.ToDecimal(form["Totalprice"]);
            Order order = new Order()
            {
                OrderId = orderid,
                TotalPrice = totalprice,
                OrderDate = DateTime.Now,
                ProductsList=cart
            };           
            

            //ShoppingCart shoppingCart = new ShoppingCart();
            //shoppingCart.productlist = cart;
            

           // shoppingCart.TotalPrice = totalprice;


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


        //[HttpPost]
        //public IActionResult CreateOrder([Bind("TotalPrice,productlist")] ShoppingCart form)
        //{


        //    decimal totalprice = form.TotalPrice;
        //    var productlist = form.productlist;

        //    return View();
        //}
    }
}