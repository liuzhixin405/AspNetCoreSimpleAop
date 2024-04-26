
using Autofac;
using IOrder.Repository;
using Order.Repository;

namespace AutofacRegister
{
    public class RepositoryModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Repository>().As<IRepository>().SingleInstance();
            builder.RegisterType<RepositoryProvider>().As<IRepositoryProvider>().InstancePerLifetimeScope();
        }
    }
}
