using Microsoft.AspNetCore.Mvc;

namespace Contract
{
    public interface IPlugin
    {
        Task<object> Add(string entity);
        Task<object> Update( string entity);
        Task<object> Get(string entity);
    }

    public class RequestBase
    {

    }

    public class ResponseBase
    {

    }
}
