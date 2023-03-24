using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AopLibrary
{
    public interface ISimpleAop
    {
        Task Before(object?[]? args);
        Task<object?> After(object? result);
    }
}

