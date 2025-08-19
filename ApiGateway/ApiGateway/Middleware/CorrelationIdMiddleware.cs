using Serilog.Context;

namespace ApiGateway.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "x-correlation-id";
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Generate if not present
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            { 
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[CorrelationIdHeader] = correlationId;
            }

            context.Response.Headers[CorrelationIdHeader] = correlationId;

            //Log for visibility
            using(LogContext.PushProperty("CorrelationId", correlationId))
            {
                _logger.LogInformation("Correlation Id set: {CorrelationId}", correlationId);
                await _next(context);
            }
        }
    }
}
