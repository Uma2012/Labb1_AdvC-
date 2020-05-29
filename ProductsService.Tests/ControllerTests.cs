using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests : IClassFixture<ProductFixture>
    {
        ProductFixture _fixture;
        public ControllerTests(ProductFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetAllProducts_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("api/product/GetAllProducts");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("api/product/GetProductBy_Id?productid=" + Guid.Empty);               
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);               
            }
        }

        [Fact]
        public async Task GetProductById_Returns_Product()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productResponse = await client.GetAsync($"/api/product/GetProductBy_Id?productid={_fixture.product.id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.product.id, product.id);
                }
            }
        }

        [Fact]
        public async Task CreateProduct_Returns_Created_Product()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid productid = Guid.Empty;
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

               var response = await client.PostAsync($"/api/product/Create", content);


                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    productid = product.id;

                    Assert.NotNull(product);
                    Assert.NotEqual<Guid>(Guid.Empty, productid);

                }

                var deleteResponse = await client.DeleteAsync($"/api/product/DeleteProduct?id={productid}");
                using (var deleteStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedid = await JsonSerializer.DeserializeAsync<Guid>(deleteStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }

            }
        }

        [Fact]
        public async Task CreateProduct_Returns_BadRequest()
        {
            Guid productid = Guid.Empty;
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                     new Product()
                     {

                     });

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/product/Create", content);
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    productid = product.id;

                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
               }
                var deleteResponse = await client.DeleteAsync($"/api/product/DeleteProduct?id={productid}");
                using (var deleteStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedid = await JsonSerializer.DeserializeAsync<Guid>(deleteStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }

            }
        }

        [Fact]
        public async Task DeleteProduct_Returns_Deleted_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid productid = Guid.Empty;
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
                var response = await client.PostAsync($"/api/product/Create", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    productid = product.id;
                }
                var deleteResponse = await client.DeleteAsync($"/api/product/DeleteProduct?id={productid}");
                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(productid, deletedId);
                }
            }

        }

        [Fact]
        public async Task DeleteProduct_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/product/DeleteProduct?id=" + Guid.Empty);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }       

    }
}
