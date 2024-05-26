using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommonFilter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class LogAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                //var filter = scope.ServiceProvider.GetRequiredService<LogFilter>();
                //return filter;

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<LogFilter>>();
                return new LogFilter(logger);
            }
        }
    }
}
