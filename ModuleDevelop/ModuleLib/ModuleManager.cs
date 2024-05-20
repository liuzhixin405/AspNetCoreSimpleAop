using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleLib
{
    public class ModuleManager
    {
        private readonly IServiceCollection _services;

        private readonly List<IModule> modules = new List<IModule>();
        public ModuleManager()
        {
            var moduleTypes = GetModuleTypes();

            foreach (var moduleType in moduleTypes)
            {
                var moduleInstance = Activator.CreateInstance(moduleType) as IModule;
                if(!modules.Contains(moduleInstance))
                modules.Add(moduleInstance);
            }
        }

        public void LoadModules(IServiceCollection services)
        {
           modules.ForEach(m => m.ConfigureService(services));
        }

        public void Configures(IApplicationBuilder app)
        {
            modules.ForEach(m => m.Configure(app));
        }
        private IEnumerable<Type> GetModuleTypes()
        {
            //return Assembly.LoadFrom("D:\\github\\AspNetCoreSimpleAop\\ModuleDevelop\\Service\\bin\\Debug\\net8.0\\Service.dll")
            //    .GetTypes()
            //    .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            return Assembly.LoadFrom("C:\\Users\\victor.liu\\Documents\\GitHub\\AspNetCoreSimpleAop\\FirstWeb\\bin\\Debug\\net8.0\\FirstWeb.dll") //实际项目不这么写
              .GetTypes()
              .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }
    }
}
