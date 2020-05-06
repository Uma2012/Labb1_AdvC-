using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Labb1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public ActionResult<List<Products>> Index()
        {
          List<Products> products=  _productRepository.GetAll();
            return View(products);
        }

       
        public IActionResult ProductDetails(Guid productid)
        {
            Products product = _productRepository.GetProductById(productid);
            return View(product);
        }
    }
}