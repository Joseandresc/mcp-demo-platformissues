using McpServer.Api.Security;

namespace McpServer.Api.Middleware;

public class SasAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SasAuthenticationMiddleware> _logger;

    public SasAuthenticationMiddleware(
        RequestDelegate next,
        ILogger<SasAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, SasTokenValidator validator)
    {
        try
        {
            var sas = context.Request.Query["sas"].ToString();

            _logger.LogDebug("Processing authentication for request: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            if (!validator.Validate(sas))
            {
                _logger.LogWarning("Unauthorized request attempt from {IpAddress} to {Path}",
                    context.Connection.RemoteIpAddress,
                    context.Request.Path);

                // Uncomment these lines when ready to enforce authentication
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized MCP request");
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SAS authentication middleware");
            throw;
        }
    }
}