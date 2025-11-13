namespace ApiGateway.Middlewares;

internal class AuthorizationHeaderApplierMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthorizationHeaderApplierMiddleware> _logger;

    public AuthorizationHeaderApplierMiddleware(RequestDelegate next, ILogger<AuthorizationHeaderApplierMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task Invoke(HttpContext context)
    {
        try
        {
            var accessToken = context.Request.Cookies["access_token"];

            if (!string.IsNullOrEmpty(accessToken)) 
                context.Request.Headers.Authorization = $"Bearer {accessToken}";

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return Task.CompletedTask;
        }
    }
}
