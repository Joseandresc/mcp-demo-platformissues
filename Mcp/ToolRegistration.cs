using McpServer.Api.Mcp.Tools;

namespace McpServer.Api.Mcp;

public static class ToolRegistration
{
    public static void Register(IServiceCollection services)
    {
        services.AddMcpServer()
            .WithHttpTransport(options =>
            {
                // Stateless mode is better for Azure App Service 
                // as it doesn't require session affinity
                options.Stateless = true;
            })
            .WithTools<IncidentTool>();
    }
}