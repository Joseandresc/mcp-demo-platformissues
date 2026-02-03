namespace McpServer.Api.Models;

public class Incident
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool PlatformIssue { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AffectedService { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public DateTime? StartTime { get; set; }
}