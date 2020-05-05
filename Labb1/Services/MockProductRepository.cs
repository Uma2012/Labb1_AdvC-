using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Labb1
{
    public class MockProductRepository : IProductRepository
    {
        private List<Products> _productList;

        //Reading the json file and store it in a object(Database object)
        public MockProductRepository()
        {
            //getting json file datá path
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\products.json");

            //reading the json file
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();

                //convert the string data to Products object
                _productList = JsonConvert.DeserializeObject<List<Products>>(json);
            }

           // _productList.OrderBy(x => x.color);

        }

        public List<Products> GetAll()
        {
            return _productList;
        }

        /// <summary>
        /// Get the product by its id
        /// </summary>
        /// <returns></returns>

        public Products GetProductById(Guid productid)
        {
            Products product = null;

            if (_productList.Any(x => x.id == productid))
            {
                product = _productList.Where(x => x.id == productid)
                   .Select(x => new Products()
                   {
                       id = x.id,
                       color = x.color,
                       description = x.description,
                       productName = x.productName,
                       price=x.price,
                       photo=x.photo
                   })
                   .FirstOrDefault();
            }

            return product;
        }   

        

     
    }
}
