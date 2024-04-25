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
            return Task.FromResult(result);
        }

        public virtual Task Before(object?[]? args)
        {
            return Task.CompletedTask;
        }
    }
}