using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labb1.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductApiHandler _productapi;

        public ProductsController(IProductRepository productRepository, ProductApiHandler productApiHandler)
        {
            this._productRepository = productRepository;
            this._productapi = productApiHandler;
        }

        //This mtd returns all the products
        [HttpGet]       
        public async Task<ActionResult<List<Products>>> Index()
        {

            List<Products> allProducts = await _productapi.GetAllAsync<Products>("https://localhost:44310/api/product");
            return View(allProducts);

            //List<Products> products=  _productRepository.GetAll();
            //return View(products);
        }

        //This mtd details of product based on incoming productid
        [HttpGet("productid")]
        public IActionResult ProductDetails(Guid productid)
        {
            Products product = _productRepository.GetProductById(productid);
            return View(product);
        }
    }
}