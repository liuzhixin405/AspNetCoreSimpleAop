using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleLib
{
    /// <summary>
    /// 可以给个抽象类，默认实现。否则各个服务每次实现接口会多做一步删除为实现接口的动作
    /// </summary>
    public interface IModule
    {
        void ConfigureService(IServiceCollection services, IConfiguration configuration=null);
        void Configure(IApplicationBuilder app, IWebHostEnvironment env = null);
    } 
}
