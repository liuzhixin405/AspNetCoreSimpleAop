using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class PluginManager
    {
        private List<IPlugin> _plugins;

        public PluginManager()
        {
            _plugins = new List<IPlugin>();
        }

        public void LoadPlugins(string path, string pattern)
        {
            // Load plugins from the specified directory
            // For simplicity, assume each plugin is a separate assembly and implement dynamic loading
            var _AssemblyLoadContext = new AssemblyLoadContext(Guid.NewGuid().ToString("N"), true);
            var files = Directory.GetFiles(path, pattern);

            foreach (var file in files)
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var assembly = _AssemblyLoadContext.LoadFromStream(fs);
                    var types = assembly.GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface);

                    foreach (var type in types)
                    {
                        var plugin = (IPlugin)Activator.CreateInstance(type);
                        _plugins.Add(plugin);
                    }
                }
            }
        }

        public IEnumerable<IPlugin> GetPlugins()
        {
            return _plugins;
        }
    }
}
