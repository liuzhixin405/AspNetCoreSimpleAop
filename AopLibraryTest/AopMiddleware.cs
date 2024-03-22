using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AopLibraryTest
{
    public class AopMiddleware
    {
        private readonly RequestDelegate _next;
        public AopMiddleware(RequestDelegate next)
        {

            _next = next;

        }

        public Task Invoke(HttpContext context)
        {
            Endpoint endpoint = context.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            if(actionDescriptor != null)
            {
                var methodInfo = actionDescriptor.MethodInfo;
               //只能拦截方法,不能拦截business业务类

            }
            //if(endpoint?.Metadata is MethodInfo methodInfo)
            //{

            //}
            return _next(context);
        }
    }
}
