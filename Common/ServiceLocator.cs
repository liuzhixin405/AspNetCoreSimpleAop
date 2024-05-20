namespace Common
{
    public class ServiceLocator  //
    {
        public static IServiceProvider Instance;    
    }

    public interface TestInterface
    {
        string TestMethod();
    }

    public class TestClass : TestInterface
    {
        public string TestMethod()
        {
            return ("Hello World!");
        }
    }
}
