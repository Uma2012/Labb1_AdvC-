using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Labb1.Models;
using Microsoft.AspNetCore.Authorization;
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


        //This mtd adds the product to session cookie.
        [Route("[controller]/AddToCart/{productid}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult AddToCart(Guid productid)
        {
            //Read the session and get the content
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(_cartName);
            List<CartItem> cartItem = new List<CartItem>();

            //if session cookie is contaisn data, assign that to new listitem
            if (currentCartItems != null)
            {
                cartItem = currentCartItems;
            }

            //if the session cookie already contains the incoming productid , then increase the already eisting amount of that product by 1
            if (currentCartItems != null && currentCartItems.Any(x => x.Product.id == productid))
            {
                int productindex = currentCartItems.FindIndex(x => x.Product.id == productid);
                currentCartItems[productindex].Amount += 1;
                cartItem = currentCartItems;
            }
            //if the session doest contain the incoming productid, then create a new item with amount =1
            else
            {
                var product = _productRepository.GetProductById(productid);
                CartItem newItem = new CartItem()
                {
                    Product = product,
                    Amount = 1
                };
                cartItem.Add(newItem);
            }

            // set the session cookie with new listofItems
            HttpContext.Session.Set(_cartName, cartItem);

            return RedirectToAction("Index", "Products");
        }

        //Reads the cart item and showing its total price
        [Route("[controller]/GetCartContent")]
        [HttpGet]
        public IActionResult GetCartContent()
        {
            //Get the items in the session cookie
            var cart = HttpContext.Session.Get<List<CartItem>>(_cartName);  
            
            ShoppingCart shoppingCart = new ShoppingCart();

            // assign the cart content to shoppingcart model's productlist
            shoppingCart.productlist = cart;

            //calculate total price only if the cart contains data
            if(cart!=null)           
            shoppingCart.TotalPrice = shoppingCart.productlist.Sum(x => x.Product.price * x.Amount);           
           
            return View(shoppingCart);
        }

        //Delete an product from session cookie based on incoming productid
        [Route("[controller]/DeleteAnItem/{id}")]
        [HttpPost]
        public IActionResult DeleteAnItem(Guid id)
        {
            //get the session cookie content
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(_cartName);

            List<CartItem> cartItem = new List<CartItem>();

            //check if the cookie already contains the incoming productid, then remove the product from sessioncookie
            if (currentCartItems != null && currentCartItems.Any(x => x.Product.id == id))
            {
                int productindex = currentCartItems.FindIndex(x => x.Product.id == id);
                currentCartItems.RemoveAt(productindex);
                cartItem = currentCartItems;
            }

            //Update the sessionCookie
            HttpContext.Session.Set(_cartName, cartItem);

            return RedirectToAction("GetCartContent", "ShoppingCart");
        }

        public IActionResult UpdateCart()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(_cartName);
            int? totalitems=0;
            if (cart == null)
                return new JsonResult(null);
            else            
               totalitems = cart.Sum(x => x.Amount);           
           

            return new JsonResult(totalitems);
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