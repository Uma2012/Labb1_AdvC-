using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductsService.Data;

namespace ProductsService.Repositories
{
    public class ProductRepository: IProductRepository
    {
        //private List<Product> _productList;

        ////Reading the json file and store it in a object(Database object)
        //public ProductRepository()
        //{

        //    //getting json file datá path
        //    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\products.json");

        //    //reading the json file
        //    using (StreamReader r = new StreamReader(path))
        //    {
        //        string json = r.ReadToEnd();

        //        //convert the string data to Products object
        //        _productList = JsonConvert.DeserializeObject<List<Product>>(json);
        //    }

        //}

        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            this._context = context;    
        }

       

        //Get all products
        public List<Product> GetAll()
        {
            var products = _context.Products.ToList();
            return products;
        }

        /// <summary>
        /// Get the product by its id
        /// </summary>
        /// <returns></returns>

        public Product GetProductById(Guid productid)
        {
            //Product product = null;

            //if (_productList.Any(x => x.id == productid))
            //{
            //    product = _productList.Where(x => x.id == productid)
            //       .Select(x => new Product()
            //       {
            //           id = x.id,
            //           color = x.color,
            //           description = x.description,
            //           productName = x.productName,
            //           price = x.price,
            //           photo = x.photo
            //       })
            //       .FirstOrDefault();
            //}

            //return product;

            var product = _context.Products.FirstOrDefault(x => x.id == productid);
            return product;



        }




    }
}

