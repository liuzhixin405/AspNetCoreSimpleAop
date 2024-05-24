using Common;
using ModuleLib;
using System.Reflection;

namespace MainHost.ServiceExtensions
{
    public static class InitModuleExt
    {
        public static void InitModule(this IServiceCollection services,IConfiguration configuration)
        {
            var modules = configuration.GetSection("Modules").Get<List<ModuleInfo>>();
            foreach (var module in modules)
            {
                GolbalConfiguration.Modules.Add(module);
                module.Assembly = Assembly.LoadFrom($"{module.Path}\\{module.Id}.dll"); //测试才这么写

                var moduleType = module.Assembly.GetTypes().FirstOrDefault(t => typeof(IModule).IsAssignableFrom(t));
                if ((moduleType != null) && (moduleType != typeof(IModule)))
                {
                    services.AddSingleton(typeof(IModule), moduleType);
                }
            }
        }
    }
}
