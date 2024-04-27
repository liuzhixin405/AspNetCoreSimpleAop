using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleLib
{
    public interface IModule
    {
        void ConfigureService(IServiceCollection services);
        void Configure(IApplicationBuilder app);
    }
}
