using Newtonsoft.Json;
using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductsService.Data
{
    public static class DbInitializer
    {
        private static List<Product> _productList;

        public static void Initialize(ProductDbContext context)
        {
            //Make sure the database has been created
            context.Database.EnsureCreated();

            //See if we already have data
            if(context.Products.Any())
            {
                return; //Db has been seeded
            }

            //Create the seed data
             
            //getting json file datá path
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\products.json");

            //reading the json file
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();

                //convert the string data to Products object
                _productList = JsonConvert.DeserializeObject<List<Product>>(json);
            }


            //Add the seed data to the context
            foreach (var product in _productList)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
 }

