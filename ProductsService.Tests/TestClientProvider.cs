using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ProductsService.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsService.Tests
{
    class TestClientProvider : IDisposable
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }


        /// <summary>
        /// Provides us with a HttpClient running on a TestServer.The Server is configured using a copy of the appsettings.json file from the Test
        /// This allows the testserver to access our database
        /// </summary>

        public TestClientProvider()
        {
            //Create a server configuration based on the appsettings.json file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();


            WebHostBuilder webHostBuilder = new WebHostBuilder();

            //Configure the services of the test server to use our dbcontext and the connectionstring from appsetting.json
            webHostBuilder.ConfigureServices(s => s.AddDbContext<ProductDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            //Use the StartUp.cs from the test-target project
            webHostBuilder.UseStartup<Startup>();

            Server = new TestServer(webHostBuilder);
            Client = Server.CreateClient();
        }
        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
