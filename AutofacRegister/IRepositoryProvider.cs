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
        public IRepository GetRepository(string x)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\lib\\{x}.Repository.dll";
            var _AssemblyLoadContext = new AssemblyLoadContext(Guid.NewGuid().ToString("N"), true);
            Assembly assembly = null;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                 assembly = _AssemblyLoadContext.LoadFromStream(fs);
            }
               
            //var assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes()
                .Where(t => typeof(IRepository).IsAssignableFrom(t) && !t.IsInterface);
            return (IRepository)Activator.CreateInstance(types.First());
        }
    }
}
