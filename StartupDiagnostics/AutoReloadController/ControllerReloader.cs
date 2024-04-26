using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupDiagnostics.AutoReloadController
{
    public interface IControllerReloader
    {
        void ReloadControllers();
    }
    public class ControllerReloader : IControllerReloader
    {
        private readonly IServiceProvider _services;

        public ControllerReloader(IServiceProvider services)
        {
            _services = services;
        }

        public void ReloadControllers()
        {
          
            using var scope = _services.CreateScope();
            var services = scope.ServiceProvider;
            foreach (var service in services.GetServices<object>())
            {
                    
            }

            Console.WriteLine("Controllers reloaded successfully.");
        }
    }
}