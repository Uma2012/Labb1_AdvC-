using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Repositories
{
      public interface IProductRepository
    {
        Product GetProductById(Guid productid);
        List<Product> GetAll();
        bool Delete(Guid id);
        Product CreateProduct(Product product);
    }
}
