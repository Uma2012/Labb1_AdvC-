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
    public class ControllerTests:IClassFixture<OrderFixture>
    {
    //     OrderFixture _fixture;
    //    public ControllerTests(OrderFixture fixture)
    //    {
    //        this._fixture = fixture;
    //    }

        //[Fact]
        //public async Task GetOrderById_Returns_NotFound()
        //{
        //    using (var client = new TestClientProvoider().Client)
        //    {
        //        var response = await client.GetAsync("api/order/GetOrderBy_Id?orderid=" + Guid.Empty);
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    }
        //}

        [Fact]
        public async Task CreateOrder_Returns_Created_Order()
        {
            using (var client = new TestClientProvoider().Client)
            {
                Guid orderid = Guid.Empty;
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {

                        OrderId = Guid.NewGuid(),
                        OrderDate = DateTime.Now,
                        //TotalItems = 5,
                        //TotalPrice = 1200

                        //TODO: how to assign values to productlist??
                        //ProductsList= new List<CartItem>()
                        //{

                        //}
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
                using (var deleteStream = await response.Content.ReadAsStreamAsync())
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

                        OrderId = Guid.NewGuid(),
                        OrderDate = DateTime.Now
                        //TotalItems = 5,
                        //TotalPrice = 1200

                        //TODO: how to assign values to productlist??
                        //ProductsList= new List<CartItem>()
                        //{

                        //}
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




    }
}
