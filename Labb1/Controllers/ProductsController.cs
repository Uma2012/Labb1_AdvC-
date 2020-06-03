using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductApiHandler _productapi;
        private readonly string _apiRootUrl;

        public ProductsController(IProductRepository productRepository, ProductApiHandler productApiHandler, IConfiguration config )
        {
            this._productRepository = productRepository;
            this._productapi = productApiHandler;
            _apiRootUrl = config.GetValue(typeof(string), "ProductApiRoot").ToString();
        }

        //Calls the ProductService to return all products
        [HttpGet]       
        public async Task<ActionResult<List<Products>>> Index()
        {

            // List<Products> allProducts = await _productapi.GetAllAsync<Products>("https://localhost:44310/api/product/GetAllProducts");
            List<Products> allProducts = await _productapi.GetAllAsync<Products>($"{_apiRootUrl}GetAllProducts");
            return View(allProducts);
           
        }

        //This mtd calls ProductService to return  Product based on its id
        [HttpGet("productid")]
        public async Task<IActionResult> ProductDetails(Guid productid)
        {
            Products product = await _productapi.GetOneAsync<Products>($"{_apiRootUrl}GetProductBy_Id?productid=" + productid);
            //Products product = await _productapi.GetOneAsync<Products>("https://localhost:44310/api/product/GetProductBy_Id?productid=" + productid);
            return View(product);

        }
    }
}