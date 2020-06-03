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
        private readonly string _apiRootUrl;

        public OrderController(IConfiguration config,UserManager<User> userManager,OrderApiHandler orderapiHandler)
        {
            this._cartName = config["CartSessionCookie:Name"];
            this._userManager = userManager;
            this._orderApiHandler = orderapiHandler;
            _apiRootUrl = config.GetValue(typeof(string), "OrderApiRoot").ToString();
        }

        //Assigning values to Order object and calling OrderService to store the Order object
        [HttpPost]
        public async Task<IActionResult> CreateOrder([Bind("TotalPrice,productlist")] ShoppingCart form)
         {       

            OrderViewModel vm = new OrderViewModel();

            //generate unique orderid (if the order contains different productid, the orderid is unique for that Order)
            Guid oriderid = Guid.NewGuid();  
           
            var productlist = form.productlist;
            
            Order createorder = null;
            //assigning values for the Order object. 
            foreach (var item in productlist)
            {
                createorder = new Order()
                {
                    OrderId = oriderid,
                    OrderDate = DateTime.Now,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    ProductId=item.Product.id  
                };

                //calling OrderSevice for storing the Order object
                await _orderApiHandler.PostAsync<Order>(createorder, $"{_apiRootUrl}CreateOrder");
                //await _orderApiHandler.PostAsync<Order>(createorder, "https://localhost:44383/api/order/CreateOrder");
            }

            //assigning totalitems to the order object
            int totalitems = 0;            
            foreach (var item in productlist)
            {
                totalitems += item.Amount;
            }
            createorder.TotalItems = totalitems;

            //assigning totalprice to the order object
            createorder.TotalPrice = form.TotalPrice;

            //assigning productlist to the order object
            createorder.ProductsList = form.productlist;

           //Retriving user information
            User user = await _userManager.GetUserAsync(User);
            vm.User = user;

            //assinging order object to viewmodel
            vm.Order = createorder;

            //Clear the session cookies once the order is created
            if (HttpContext.Session.GetString(_cartName) != null)
                HttpContext.Session.Remove(_cartName);

            return View(vm);
        }
    }
}