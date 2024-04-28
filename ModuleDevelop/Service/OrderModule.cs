using IOrderService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModuleLib;
using OrderService;

namespace OrderModule
{
    public class Module : IModule
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<OrderFilterMiddelware>();   
        }

        public void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<IService, Service>();
        }
    }
}
