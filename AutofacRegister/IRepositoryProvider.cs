using IOrder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;

namespace AutofacRegister
{
    public interface IRepositoryProvider
    {
        IRepository GetRepository(string serviceeName);
    }
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly Dictionary<string, (Assembly assembly, DateTime lastModified)> _assemblyCache = new Dictionary<string, (Assembly assembly, DateTime lastModified)>();
        private readonly Dictionary<string, IRepository> _typeCache = new Dictionary<string, IRepository>();

        public IRepository GetRepository(string serviceName)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\lib\\{serviceName}.Repository.dll";
            var lastModified = File.GetLastWriteTimeUtc(path);
            if (_assemblyCache.TryGetValue(path, out var cachedEntry) && cachedEntry.lastModified == lastModified)
            {
                // 使用缓存中的 Assembly 对象
                return CreateInstanceFromAssembly(cachedEntry.assembly,serviceName);
            }
            else
            {
                // 加载并缓存新的 Assembly 对象
                var assembly = LoadAssemblyFromFile(path);
                _assemblyCache[path] = (assembly, lastModified);
                return CreateInstanceFromAssembly(assembly,serviceName);
            }
        }

        private Assembly LoadAssemblyFromFile(string path)
        {
            var _AssemblyLoadContext = new AssemblyLoadContext(Guid.NewGuid().ToString("N"), true);
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return _AssemblyLoadContext.LoadFromStream(fs);
            }
        }
        private IRepository CreateInstanceFromAssembly(Assembly assembly,string serviceName)
        {
            var  type_key = $"{assembly.FullName}_{serviceName}";
            if(_typeCache.TryGetValue(type_key, out var cachedType))
            {
                return _typeCache[type_key];
            }
            var type = assembly.GetTypes()
                .Where(t => typeof(IRepository).IsAssignableFrom(t) && !t.IsInterface)
                .FirstOrDefault();

            if (type != null)
            {
                var instance= (IRepository)Activator.CreateInstance(type);
                _typeCache[type_key] = instance;
                return instance;
            }
            else
            {
                throw new InvalidOperationException("No suitable type found in the assembly.");
            }
        }
    }
}
