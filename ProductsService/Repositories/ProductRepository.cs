using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Repositories
{
    public class ProductRepository
    {
        public Product GetById(Guid Id)
        {
            return new Product();
        }
    }
}
