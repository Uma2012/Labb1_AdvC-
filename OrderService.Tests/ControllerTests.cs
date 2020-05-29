using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests
{
    public class ControllerTests
    {  

        [Fact]
        public async Task CreateOrder_Returns_Created_Order()
        {
            using (var client = new TestClientProvoider().Client)
            {
                Guid orderid = Guid.Empty;
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        OrderId = Guid.Parse("2ae4bb3a-9664-4235-b721-af45dfa7d81a"),
                        OrderDate = DateTime.Now,
                        UserId=Guid.Parse("bbd9482e-1193-4545-88b7-81aa97ebfa77"),
                        ProductId=Guid.Parse("08b446ae-03bb-4e53-96de-c34f31a79f09")
                    }
                    );
            

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/order/createorder", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    orderid = order.OrderId; 

                    Assert.NotNull(order);
                    Assert.NotEqual<Guid>(Guid.Empty, orderid);

                }

                var deleteResponse = await client.DeleteAsync($"/api/order/deleteorder?id={orderid}");
                using (var deleteStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedid = await JsonSerializer.DeserializeAsync<Guid>(deleteStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }


                }
        }

        [Fact]
        public async Task CreateOrder_Returns_BadRequest()
        {
            Guid orderid = Guid.Empty;
            using (var client = new TestClientProvoider().Client)
            {
                var payload = JsonSerializer.Serialize(
                     new Order()
                     {

                     });

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/order/Createorder", content);
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    orderid = order.OrderId;

                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                }
                var deleteResponse = await client.DeleteAsync($"/api/order/deleteorder?id={orderid}");
                using (var deleteStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedid = await JsonSerializer.DeserializeAsync<Guid>(deleteStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }


            }
        }


        [Fact]
        public async Task DeleteOrder_Returns_Deleted_Id()
        {
            using (var client = new TestClientProvoider().Client)
            {
                Guid orderid = Guid.Empty;
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        OrderId = Guid.Parse("2ae4bb3a-9664-4235-b721-af45dfa7d81a"),
                        OrderDate = DateTime.Now,
                        UserId = Guid.Parse("bbd9482e-1193-4545-88b7-81aa97ebfa77"),
                        ProductId = Guid.Parse("08b446ae-03bb-4e53-96de-c34f31a79f09")
                    }
                    );

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/order/createorder", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    orderid = order.OrderId;
                }
                var deleteResponse = await client.DeleteAsync($"/api/order/deleteorder?id={orderid}");
                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(orderid, deletedId);
                }
            }

            }

        [Fact]
        public async Task DeleteOrder_Returns_Notfound()
        {
            using (var client = new TestClientProvoider().Client)
            {
                var response = await client.DeleteAsync("/api/order/deleteorder?id=" + Guid.Empty);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
        }




    }
}
