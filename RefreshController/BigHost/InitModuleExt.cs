using Common;
using ModuleLib;
using System.Reflection;
using System.Runtime.Loader;

namespace BigHost
{
    public static class InitModuleExt
    {
        public static void InitModule(this IServiceCollection services,IConfiguration configuration)
        {
            var modules = configuration.GetSection("Modules").Get<List<ModuleInfo>>();
            foreach (var module in modules)
            {
                GolbalConfiguration.Modules.Add(module);
                using (var fileStream = File.OpenRead($"{module.Path}\\{module.Id}.dll"))
                {
                    module.Assembly = AssemblyLoadContext.Default.LoadFromStream(fileStream); // 测试才这么写

                    var moduleType = module.Assembly.GetTypes().FirstOrDefault(t => typeof(IModule).IsAssignableFrom(t));
                    if ((moduleType != null) && (moduleType != typeof(IModule)))
                    {
                        services.AddSingleton(typeof(IModule), moduleType);
                    }
                }
            }
        }
    }
}
