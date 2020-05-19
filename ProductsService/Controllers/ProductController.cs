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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]     
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = _productRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Product/95e87976-88e3-415d-b139-219538e948c1
        [HttpGet("{productid}")]
        public ActionResult<Product> GetProductBy_Id(Guid productid)
        {
            Product product = _productRepository.GetProductById(productid);
            return Ok(product);
        }
    }
}