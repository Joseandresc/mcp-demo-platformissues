using McpServer.Api.Mcp.Tools;

namespace McpServer.Api.Mcp;

public static class ToolRegistration
{
    public static void Register(IServiceCollection services)
    {
        services.AddMcpServer()
            .WithHttpTransport()
            .WithTools<IncidentTool>();
    }
}