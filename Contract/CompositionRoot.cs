using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class CompositionRoot
    {
        private static Autofac.IContainer _container;
        public static void SetContainer(Autofac.IContainer container) => _container = container;
        public static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}
