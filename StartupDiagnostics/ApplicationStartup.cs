using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using AutofacRegister;
using CommonServiceLocator;
using Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupDiagnostics
{
    public static class ApplicationStartup
    {
        public static IServiceCollection Initialize(this
           IServiceCollection services
        )
        {
           

            var serviceProvider = CreateAutofacServiceProvider(
                services
              );

            return serviceProvider;
        }

        private static IServiceCollection CreateAutofacServiceProvider(
            IServiceCollection services
          )
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new RepositoryModule());
           

           

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            var serviceProvider = new AutofacServiceProvider(buildContainer);

            CompositionRoot.SetContainer(buildContainer);
            return services;
        }
    }

   
}
