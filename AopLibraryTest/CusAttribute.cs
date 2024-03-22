using Metalama.Framework.Aspects;

namespace AopLibraryTest
{
    public class CusAttribute : OverrideMethodAspect   //拦截方法
    {
        private readonly string name;
        public CusAttribute(string name)
        {

            this.name = name;

        }
        public override dynamic OverrideMethod()
        {
            Console.WriteLine(meta.Target.Method.ToDisplayString() + " started."+name);

            try
            {
                var result = meta.Proceed();

                Console.WriteLine(meta.Target.Method.ToDisplayString() + " succeeded.");

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(meta.Target.Method.ToDisplayString() + " failed: " + e.Message);

                throw;
            }
        }
    }
}
