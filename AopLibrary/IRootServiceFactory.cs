using System;
using System.Threading.Tasks;

namespace AopLibrary
{
    public interface IRootServiceFactory<T>
    {
        Task<TResponse?> Invoke<TResponse>(string methodName, object?[]? args);
        Task Invoke(string methodName, object?[]? args);
    }
}
