using McpServer.Api.Models;
using McpServer.Api.Services.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Api.Mcp.Tools;

public class IncidentTool
{
    private readonly IIncidentService _service;
    private readonly ILogger<IncidentTool> _logger;

    public IncidentTool(
        IIncidentService service,
        ILogger<IncidentTool> logger)
    {
        _service = service;
        _logger = logger;
    }

    [McpServerTool(Name = "get_incidentcausedbyplatform"), Description("identify if the resource is impacted by azure platform")]
    public async Task<Incident> ExecuteAsync(string resourceId)
    {
        try
        {
            _logger.LogInformation("MCP Tool 'get_incidentcausedbyplatform' called with ResourceId: {ResourceId}", resourceId);

            if (string.IsNullOrWhiteSpace(resourceId))
            {
                _logger.LogWarning("MCP Tool 'get_incident' called with null or empty incident ID");
                throw new ArgumentException("Incident ID cannot be null or empty", nameof(resourceId));
            }

            var result = await _service.GetIncidentAsync(resourceId);

            _logger.LogInformation("MCP Tool 'get_incidentcausedbyplatform' successfully returned resourceId: {resourceId}", resourceId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing MCP Tool 'get_incidentcausedbyplatform' for resourceId: {resourceId}", resourceId);
            throw;
        }
    }
}