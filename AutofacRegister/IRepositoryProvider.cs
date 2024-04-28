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

        public IRepository GetRepository(string x)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\lib\\{x}.Repository.dll";
             var lastModified = File.GetLastWriteTimeUtc(path);
            if (_assemblyCache.TryGetValue(path, out var cachedEntry) && cachedEntry.lastModified == lastModified)
        {
            // 使用缓存中的 Assembly 对象
            return CreateInstanceFromAssembly(cachedEntry.assembly);
        }
        else
        {
            // 加载并缓存新的 Assembly 对象
            var assembly = LoadAssemblyFromFile(path);
            _assemblyCache[path] = (assembly, lastModified);
            return CreateInstanceFromAssembly(assembly);
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
        private IRepository CreateInstanceFromAssembly(Assembly assembly)
    {
        var type = assembly.GetTypes()
            .Where(t => typeof(IRepository).IsAssignableFrom(t) && !t.IsInterface)
            .FirstOrDefault();
        
        if (type != null)
        {
            return (IRepository)Activator.CreateInstance(type);
        }
        else
        {
            throw new InvalidOperationException("No suitable type found in the assembly.");
        }
    }
    }
}
