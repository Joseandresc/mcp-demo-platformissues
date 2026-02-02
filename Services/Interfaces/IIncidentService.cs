using McpServer.Api.Models;

namespace McpServer.Api.Services.Interfaces;

public interface IIncidentService
{
    Task<Incident> GetIncidentAsync(string id);
}