using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionAttribute
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CusServiceAttribute:Attribute
    {
        public CusServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }
        public CusServiceAttribute(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
        public Type ServiceType { get; }
        public ServiceLifetime Lifetime { get; }
    }
}
