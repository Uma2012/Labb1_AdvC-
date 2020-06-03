using Labb1.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb1.Services
{
    public class OrderApiHandler
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string orderApiKey;
        public OrderApiHandler(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this._clientFactory = clientFactory;
            orderApiKey = config.GetValue<string>("ApiKeys:OrderApiKey");

        }
        public async Task PostAsync<T>(T obj, string webApiPath) 
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, webApiPath);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Labb1");
            request.Headers.Add("Api_Key", orderApiKey);

            // Serialize object to JSON
             var postJson = JsonSerializer.Serialize(obj);           
            request.Content = new StringContent(postJson, Encoding.UTF8, "application/json");

            // Send and receive request
            var result = await client.SendAsync(request);
            var responseString = await result.Content.ReadAsStreamAsync();

            Order order = await JsonSerializer.DeserializeAsync<Order>(responseString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        

             

        }
    }
}
