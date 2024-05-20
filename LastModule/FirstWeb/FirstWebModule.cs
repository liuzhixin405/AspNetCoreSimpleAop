using Common;
using Microsoft.AspNetCore.Builder;
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
        public void Configure(IApplicationBuilder app)
        {
            WebApplication webApplication = app as WebApplication;
    
            webApplication.MapGet("name/", (IService service) => service.Name());   //该控制器可用         
        }

        public void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IService,NameService>(); // 只有在上面的Map.Get最小api中才有效果，可以是该dll加载了两次导致的。 该使用方式搭配最小api,否则使用属性注入比较合适
            services.AddTransient<TestInterface, TestClass>();   //新类库引用即可。
        }
    }
}
