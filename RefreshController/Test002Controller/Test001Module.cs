﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test002Controller
{
    public class Test001Module : IModule
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env = null)
        {
            
        }

        public void ConfigureService(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddTransient<IBookService, BookService>();
        }
    }
}
