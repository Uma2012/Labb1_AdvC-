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

        // GET: api/Product
        [HttpGet]     
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = _productRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Product/GetProductBy_Id?productid=95e87976-88e3-415d-b139-219538e948c1
        [HttpGet]
        public ActionResult<Product> GetProductBy_Id(Guid productid)
        {
            Product product = _productRepository.GetProductById(productid);
            if (product != null)
                return Ok(product);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            var createdProduct = _productRepository.CreateProduct(product);
            if (createdProduct != null)
            {
                return Ok(createdProduct);
            }
            else
                return BadRequest();

            //if (product == null)
            //    return BadRequest();
            //var createdProduct = _productRepository.CreateProduct(product);
            //return Ok(product);

        }

        [HttpDelete]
        public ActionResult<Guid> DeleteProduct(Guid id)
        {
            var wasDeleted = _productRepository.Delete(id);
            if (wasDeleted)
            {
                return Ok(id);
            }
            else
                return NotFound(id);
        }
    }
}