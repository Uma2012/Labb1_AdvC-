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

        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            this._context = context;    
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
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
            var product = _context.Products.FirstOrDefault(x => x.id == productid);
            return product;

        }

        /// <summary>
        /// store the product to the database 
        /// </summary>
        /// <returns>Product</returns>        
        public Product CreateProduct(Product product)
        {
            if (product.productName == null || product.publishDate == null || product.price == 0)
                return product = null;
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Delete the Product based on incoming Products's id
        public bool Delete(Guid id)
        {
            try
            {
                var product = GetProductById(id);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



       

       




    }
}

