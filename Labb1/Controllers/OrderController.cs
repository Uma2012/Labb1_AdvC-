using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Models;
using Labb1.Services;
using Labb1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    public class OrderController : Controller
    {
        private readonly string _cartName;
        private readonly UserManager<User> _userManager;
        private readonly OrderApiHandler _orderApiHandler;
        private readonly ProductApiHandler _productapi;

        public OrderController(IConfiguration config,UserManager<User> userManager,OrderApiHandler orderapiHandler, ProductApiHandler productApiHandler)
        {
            this._cartName = config["CartSessionCookie:Name"];
            this._userManager = userManager;
            this._orderApiHandler = orderapiHandler;
            this._productapi = productApiHandler;
        }

        //This mtd calculate total number of products in the session
        //Creats order
        //Get the User information
        [HttpPost]
        public async Task<IActionResult> CreateOrder([Bind("TotalPrice,productlist")] ShoppingCart form)
         {
        //    List<Guid> productids = new List<Guid>();
        //    foreach (var item in form.productlist)
        //    {
        //        Products product = await _productapi.GetOneAsync<Products>("https://localhost:44310/api/product/GetProductBy_Id/" + item);
        //        productids.Add(product.id);
        //    }
        //    foreach (var item in productids)
        //    {
        //        var cr = new Order()
        //        {
        //            ProductId=item
        //        };
        //    }


            OrderViewModel vm = new OrderViewModel();
            Guid oriderid = Guid.NewGuid();  //generate unique orderid (if the order contains different productid, the orderid is unique for that Order)
           
            var productlist = form.productlist;
           
            
            Order createorder = null;

            foreach (var item in productlist)
            {
                createorder = new Order()
                {
                    OrderId = oriderid,
                    OrderDate = DateTime.Now,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    ProductId=item.Product.id  

                };
                await _orderApiHandler.PostAsync<Order>(createorder, "https://localhost:44383/api/order/CreateOrder");

            }

            //calculate total items of the Order
            int totalitems = 0;            
            foreach (var item in productlist)
            {
                totalitems += item.Amount;
            }
            createorder.TotalItems = totalitems;

            
           // decimal totalprice = form.TotalPrice;
            createorder.TotalPrice = form.TotalPrice;
            createorder.ProductsList = form.productlist;

            vm.Order = createorder;
            User user = await _userManager.GetUserAsync(User);
            vm.User = user;

            //Clear the session cookies once the order is created
            if (HttpContext.Session.GetString(_cartName) != null)
                HttpContext.Session.Remove(_cartName);

            return View(vm);
        }
    }
}