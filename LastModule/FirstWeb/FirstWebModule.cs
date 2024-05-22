using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWeb
{
    public class FirstWebModule : IModule
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env = null)
        {
            WebApplication newApp = app as WebApplication;
            var group = newApp.MapGroup("001");
            group.MapGet("name/", (IService service) => service.Name()).WithTags("大标题1");   //该控制器可用
            var group2 = newApp.MapGroup("002");
            group2.MapGet("test/", () => "hello test").WithTags("大标题2");   //该控制器可用
        }


        public void ConfigureService(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<IService, NameService>(); // 只有在上面的Map.Get最小api中才有效果，可以是该dll加载了两次导致的。 该使用方式搭配最小api,否则使用属性注入比较合适
            services.AddTransient<TestInterface, TestClass>();   //新类库引用即可。
        }
    }
}
