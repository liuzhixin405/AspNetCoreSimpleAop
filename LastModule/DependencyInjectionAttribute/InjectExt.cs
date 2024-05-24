using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionAttribute
{
    public static class InjectExt
    {
        public static void ReisterServiceFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var typesWithServiceAttribute = assembly.GetTypes().Where(t => t.GetCustomAttributes<CusServiceAttribute>().Any());

            foreach (var type in typesWithServiceAttribute)
            {
                var servceAttribute = type.GetCustomAttribute<CusServiceAttribute>();
                services.Add(new ServiceDescriptor(servceAttribute.ServiceType ?? type, type, servceAttribute.Lifetime));
            }
        }
    }
}
