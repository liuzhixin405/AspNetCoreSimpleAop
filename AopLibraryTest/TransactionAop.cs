using AopLibrary;

namespace AopLibraryTest
{
    public class TransactionAop : ISimpleAop
    {
        public async Task<object?> After(object? result)
        {
            Console.WriteLine("事务开启");
            return result;
        }

        public async Task Before(object?[]? args)
        {
            Console.WriteLine("事务结束");
            await Task.CompletedTask;
        }
    }
}
