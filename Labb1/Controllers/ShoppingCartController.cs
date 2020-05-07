using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Labb1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Labb1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly string _cartName;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IProductRepository productRepository,IConfiguration config )
        {
            this._cartName = config["CartSessionCookie:Name"];
            this._productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult GetCartContent()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(_cartName);
           
            var products = _productRepository.GetAll();

            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.productlist = cart;
            if(cart!=null)            

           shoppingCart.TotalPrice = shoppingCart.productlist.Sum(x => x.Product.price * x.Amount);
           
            return View(shoppingCart);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid productid)
        {        
        
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(_cartName);
            List<CartItem> cartItem = new List<CartItem>();

            if(currentCartItems!=null)
            {
                cartItem = currentCartItems;
            }

            if(currentCartItems!=null && currentCartItems.Any(x=>x.Product.id==productid))
            {
                int productindex = currentCartItems.FindIndex(x => x.Product.id == productid);
                currentCartItems[productindex].Amount += 1;
                cartItem = currentCartItems;
            }
            else
            {
                var product = _productRepository.GetProductById(productid);
                CartItem newItem = new CartItem()
                {
                    Product = product,
                    Amount=1

                };
                cartItem.Add(newItem);
            }         


            HttpContext.Session.Set<List<CartItem>>(_cartName, cartItem);

            return RedirectToAction("Index", "Products");
        }
        public IActionResult DeleteAnItem(int productid)
        {
            return View();
        }


    }



    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}