namespace DynamicServiceDemo.AssemblyExtensions
{
    public class ExternalContexts
    {
        private static Dictionary<string,CollectibleAssemblyLoadContext>? _external = null;

        static ExternalContexts()
        {
            _external = new Dictionary<string, CollectibleAssemblyLoadContext>();
        }

        public static bool Any(string externalName) 
        { 
            return _external!.ContainsKey(externalName);
        }

        public static void Remove(string externalName)
        {
            if (_external!.ContainsKey(externalName))
            {
                _external[externalName].Unload();
                _external.Remove(externalName);
            }
        }

        public static CollectibleAssemblyLoadContext Get(string externalName)
        {
            return _external![externalName];
        }

        public static void Add(string externalName, CollectibleAssemblyLoadContext loadContext)
        {
            _external!.Add(externalName, loadContext);
        }
    }
}
