using System.Runtime.Loader;

namespace DynamicServiceDemo.AssemblyExtensions
{
    public class CollectibleAssemblyLoadContext: AssemblyLoadContext
    {
        public CollectibleAssemblyLoadContext():base(isCollectible: true)
        {
            
        }
    }
}
