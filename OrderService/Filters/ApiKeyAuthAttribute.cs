using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Filters
{    
        /// <summary>
        /// An attribute looks for an apikey header and validates it against a key stored in appsettings
        /// </summary>

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //Specifies where we can use this attribute
        public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
        {
            private const string API_KEY_HEADER_NAME = "Api_Key";

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var potentialApiKey))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                //Get the IConfiguration service this way because we cant use a constructor here
                var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var apiKey = configuration.GetValue<string>("ApiKey"); //Name of the property in appsettings

                if (!apiKey.Equals(potentialApiKey))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                await next(); //Run the next middleware in the chain
            }
        }
    }

