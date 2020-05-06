using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1.Controllers
{
    public class OrderController : Controller
    {
        //[HttpPost]
        //public IActionResult CreateOrder([FromForm]ShoppingCart form)
        //{

        //    // decimal totalprice=Convert.ToDecimal(form["totalprice"]);
        //    decimal totalprice = form.TotalPrice;

        //    return View();
        //}

        [HttpPost]
        //public IActionResult CreateOrder(IFormCollection form)
        //{

        //    decimal totalprice = Convert.ToDecimal(form["totalprice"]);
        //    var productList = form["productlist"];
        //    foreach(var item in productList)
        //    {
        //       var a= item;
        //    }


        //    return View();
        //}


        [HttpPost]
        public IActionResult CreateOrder([Bind("TotalPrice,productlist")] ShoppingCart form)
        {

            
            decimal totalprice = form.TotalPrice;
            var productlist = form.productlist;

            return View();
        }
    }
}