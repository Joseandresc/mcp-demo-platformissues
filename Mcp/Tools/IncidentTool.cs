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

    [McpServerTool(Name = "get_incidentcausedbyplatformAppService"), Description("identify if an App Service resource is impacted by azure platform issues")]
    public Task<Incident> GetAppServiceIncidentAsync(string resourceId)
    {
        _logger.LogInformation("MCP Tool 'get_incidentcausedbyplatformAppService' called with ResourceId: {ResourceId}", resourceId);

        var result = new Incident
        {
            Id = resourceId,
            Status = "Active",
            Severity = "Sev2",
            PlatformIssue = true,
            Description = "App Service platform issue detected. The underlying infrastructure experienced a transient failure affecting web app availability.",
            AffectedService = "App Service",
            Region = "West US 2",
            StartTime = DateTime.UtcNow.AddHours(-2)
        };

        return Task.FromResult(result);
    }

    [McpServerTool(Name = "get_incidentcausedbyplatformServiceBus"), Description("identify if a Service Bus resource is impacted by azure platform issues")]
    public Task<Incident> GetServiceBusIncidentAsync(string resourceId)
    {
        _logger.LogInformation("MCP Tool 'get_incidentcausedbyplatformServiceBus' called with ResourceId: {ResourceId}", resourceId);

        var result = new Incident
        {
            Id = resourceId,
            Status = "Mitigated",
            Severity = "Sev3",
            PlatformIssue = true,
            Description = "Service Bus platform issue detected. Message delivery experienced delays due to backend infrastructure maintenance.",
            AffectedService = "Service Bus",
            Region = "East US",
            StartTime = DateTime.UtcNow.AddHours(-4)
        };

        return Task.FromResult(result);
    }

    [McpServerTool(Name = "get_incidentcausedbyplatformSQL"), Description("identify if a SQL Database resource is impacted by azure platform issues")]
    public Task<Incident> GetSqlIncidentAsync(string resourceId)
    {
        _logger.LogInformation("MCP Tool 'get_incidentcausedbyplatformSQL' called with ResourceId: {ResourceId}", resourceId);

        var result = new Incident
        {
            Id = resourceId,
            Status = "Active",
            Severity = "Sev1",
            PlatformIssue = true,
            Description = "SQL Database platform issue detected. Database connectivity impacted due to storage subsystem degradation.",
            AffectedService = "SQL Database",
            Region = "Central US",
            StartTime = DateTime.UtcNow.AddMinutes(-45)
        };

        return Task.FromResult(result);
    }
}