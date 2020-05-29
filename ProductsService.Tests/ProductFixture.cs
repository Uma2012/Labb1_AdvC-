using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductsService.Tests
{
    public class ProductFixture : IDisposable
    {
        public Product product { get; private set; }

        public ProductFixture()
        {
            product =  Initialize().Result;
        }

        private async Task<Product> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Product()
                    {
                        productName = "Test Product",                      
                        description = "Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.\n\nDonec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque.",
                        color = "Crimson",
                        publishDate = Convert.ToDateTime("2019-07-07T22:17:03Z"),
                        price = 200,
                        photo = "material_1.jpg"
                    }
                    );


                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/product/create", content);

                using(var responseStream=await response.Content.ReadAsStreamAsync())
                {
                    var createProduct = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return createProduct;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/product/deleteproduct?id={product.id}");
                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                                       new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
