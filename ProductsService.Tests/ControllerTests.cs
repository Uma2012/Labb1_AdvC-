using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests/* : IClassFixture<ProductFixture>*/
    {
        //ProductFixture _fixture;
        //public ControllerTests(ProductFixture fixture)
        //{
        //    this._fixture = fixture;
        //}

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

        //[Fact]
        //public async Task GetProductById_Returns_Product()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var productResponse = await client.GetAsync($"/api/product?productid={_fixture.product.id}");

        //        using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
        //        {
        //            var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
        //                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        //            Assert.Equal(_fixture.product.id, product.id);
        //        }

        //    }

        //}

    }
}
