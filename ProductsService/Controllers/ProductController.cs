using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Models;
using ProductsService.Repositories;

namespace ProductsService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet]     
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = _productRepository.GetAll();
            return Ok(products);
        }

       [HttpGet("productid")]
        public IActionResult GetProductBy_Id(Guid productid)
        {
            Product product = _productRepository.GetProductById(productid);
            return Ok(product);
        }
    }
}