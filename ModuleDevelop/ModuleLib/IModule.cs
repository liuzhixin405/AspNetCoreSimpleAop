using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleLib
{
    public interface IModule
    {
        void ConfigureService(IServiceCollection services, IConfiguration configuration=null);
        void Configure(IApplicationBuilder app, IWebHostEnvironment env = null);
    }
}
