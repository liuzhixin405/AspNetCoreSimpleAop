using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule
{
    public class OrderFilterMiddelware
    {
        private readonly RequestDelegate _next;

        public OrderFilterMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("OrderFilterMiddelware");
            // Do something before the request
            await _next(context);
            // Do something after the request
        }
    }
}
