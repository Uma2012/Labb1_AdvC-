using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1
{
    public interface IProductRepository
    {      
        Products GetProductById(Guid productid);
        List<Products> GetAll();
       
    }
}
