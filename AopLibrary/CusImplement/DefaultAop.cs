using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AopLibrary.CusImplement
{
    public class DefaultAOP : ISimpleAop
    {
        public virtual Task<object?> After(object? result)
        {
            Console.WriteLine("默认的aop执行后");
            return Task.FromResult(result);
        }

        public virtual Task Before(object?[]? args)
        {
            Console.WriteLine("默认的aop,执行前");
            return Task.CompletedTask;
        }
    }
}