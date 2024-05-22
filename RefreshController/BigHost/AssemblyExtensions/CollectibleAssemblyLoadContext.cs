using System.Runtime.Loader;

namespace BigHost.AssemblyExtensions
{
    public class CollectibleAssemblyLoadContext: AssemblyLoadContext
    {
        public CollectibleAssemblyLoadContext():base(isCollectible: true)
        {
            
        }
    }
}
