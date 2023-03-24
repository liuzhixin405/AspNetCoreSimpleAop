using AopLibrary.CusImplement;

namespace AopLibraryTest
{
    public class ReplaceDefaultAop:DefaultAOP
    {
       
        public override Task Before(object?[]? args)
        {
            Console.WriteLine($"方法调用之前{DateTime.UtcNow}");
            return base.Before(args);
        }
        public override Task<object?> After(object? result)
        {
            Console.WriteLine($"方法调用之后{DateTime.UtcNow}");
            return base.After(result);
        }
    }
}
