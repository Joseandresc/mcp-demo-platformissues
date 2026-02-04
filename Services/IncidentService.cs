using McpServer.Api.Models;
using McpServer.Api.Services.Interfaces;
using McpServer.Api.Exceptions;

namespace McpServer.Api.Services;

public class IncidentService : IIncidentService
{
    private readonly Random _random = new();
    private readonly ILogger<IncidentService> _logger;

    public IncidentService(ILogger<IncidentService> logger)
    {
        _logger = logger;
    }

    public Task<Incident> GetIncidentAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving incident with ID: {IncidentId}", id);

            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Attempted to retrieve incident with null or empty ID");
                throw new ArgumentException("Incident ID cannot be null or empty", nameof(id));
            }

            var incident = new Incident
            {
                Id = id,
                Status = "Mitigated",
                Severity = "Sev2",
                PlatformIssue = true,
                Description = "Platform issue detected. The resource was impacted by underlying Azure infrastructure.",
                AffectedService = "Azure Platform",
                Region = "Global"
            };

            _logger.LogInformation(
                "Successfully retrieved incident. ID: {IncidentId}, Status: {Status}, Severity: {Severity}, PlatformIssue: {PlatformIssue}",
                incident.Id, incident.Status, incident.Severity, incident.PlatformIssue);

            return Task.FromResult(incident);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            _logger.LogError(ex, "Error retrieving incident with ID: {IncidentId}", id);
            throw;
        }
    }
}