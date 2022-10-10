namespace BookStore.Host.Test
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method.Contains("GET"))
            {
                _logger.LogInformation($"Invoked my {nameof(LogMiddleware)} method:{httpContext.Request.Method}");
            }
            await _next(httpContext);
        }

    }
}
