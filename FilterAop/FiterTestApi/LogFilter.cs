using Microsoft.AspNetCore.Mvc.Filters;

namespace FiterTestApi
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
            _logger.LogInformation("OnActionExecutionAsync");
            await next();
            _logger.LogInformation("OnActionExecutionAsync End");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("OnResultExecutionAsync");
            await next();
            _logger.LogInformation("OnResultExecutionAsync End");
        }
    }
}
