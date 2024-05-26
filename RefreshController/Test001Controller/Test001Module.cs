using Business;
using IBusiness;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test001Controller
{
    //public class Test001Module : IModule
    //{
    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env = null)
    //    {
            
    //    }

    //    public void ConfigureService(IServiceCollection services, IConfiguration configuration = null)
    //    {
    //        //services.AddTransient<IAnimalService, Dog>();
    //        //services.AddTransient<IProductBusiness, ProductBusiness>(); //需要host 程序集有直接引用,否则找不到对象,还不如直接在Controller中构造 模块化开发遇到热拔插
    //    }
    //}
}
