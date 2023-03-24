using AopLibrary.CusImplement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AopLibrary
{
    public static class Extensions
    {
        public static IServiceCollection AddSimpleAop(this IServiceCollection service)
        {
            service.AddSingleton(typeof(IRootServiceFactory<>), typeof(RootServiceFactory<>));
            service.AddSingleton(typeof(ISimpleAop), typeof(DefaultAOP));
            return service;
        }
    }
}
