using AopLibrary;
using AopLibrary.CusImplement;

namespace AopLibraryTest
{
    public class LogAop:ISimpleAop
    {
       
        public  Task Before(object?[]? args)
        {
            //Console.WriteLine($"方法调用之前{DateTime.UtcNow} ,parameters:{string.Join(",",args.Select(x=>x.ToString()))}");
            //return base.Before(args);
            Console.WriteLine("记录日志 操作前");
            return Task.CompletedTask;
        }
        public  async Task<object?> After(object? result)
        {
            //Console.WriteLine($"方法调用之后{DateTime.UtcNow},result:{result}");
            //return base.After(result);
            Console.WriteLine("记录日志 操作完成后");
            await Task.CompletedTask;
            return result;
        }
    }
}
