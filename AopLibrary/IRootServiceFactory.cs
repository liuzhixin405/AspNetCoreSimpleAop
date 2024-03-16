using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AopLibrary
{
    public interface IRootServiceFactory<T>
    {
        Task<TResponse?> Invoke<TResponse>(string methodName, object?[]? args);
        Task Invoke(string methodName, object?[]? args);

        IRootServiceFactory<T> AddAop(ISimpleAop simpleAop);
       
    }
}
