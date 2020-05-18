using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb1.Services
{
    public class ProductApiHandler
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductApiHandler(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }
        public async Task<List<T>> GetAllAsync<T>(string webApipath)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, webApipath);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Labb1");

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var products = await JsonSerializer.DeserializeAsync<List<T>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return products;
            }

            return null;

        }
    }
}
