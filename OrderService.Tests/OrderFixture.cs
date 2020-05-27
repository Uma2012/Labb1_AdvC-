using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Tests
{
    public class OrderFixture
    {
        public Order order { get; private set; }

        public OrderFixture()
        {
            order = Initialize().Result;
        }

        private async Task<Order> Initialize()
        {
            using (var client = new TestClientProvoider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        OrderDate = DateTime.Now,
                        UserId = Guid.Parse("bbd9482e-1193-4545-88b7-81aa97ebfa77"),
                        ProductId = Guid.Parse("08b446ae-03bb-4e53-96de-c34f31a79f09")
                    }
                    );



                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/order/createorder", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var createProduct = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return createProduct;
                }


            }

        }

        public async void Dispose()
        {
            using (var client = new TestClientProvoider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/order/deleteorder?id={order.OrderId}");
                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
