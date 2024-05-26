using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CommonFilter
{
    public class LogFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private readonly ILogger<LogFilter> _logger;
        public LogFilter( ILogger<LogFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"OnActionExecutionAsync: {context.ActionDescriptor.DisplayName},datetime:{DateTime.Now}");
            await next();
            _logger.LogInformation($"OnActionExecutionAsync End: {context.ActionDescriptor.DisplayName},datetime:{DateTime.Now}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation($"OnResultExecutionAsync: {context.ActionDescriptor.DisplayName},datetime:{DateTime.Now}");
            await next();
            _logger.LogInformation($"OnResultExecutionAsync End: {context.ActionDescriptor.DisplayName},datetime:{DateTime.Now}");
        }
    }
}
