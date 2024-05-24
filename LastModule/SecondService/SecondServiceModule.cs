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

namespace SecondService
{
    internal class SecondServiceModule : IModule
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env = null)
        {
           
        }

        public void ConfigureService(IServiceCollection services, IConfiguration configuration = null)
        {
            
        }
    }
}
