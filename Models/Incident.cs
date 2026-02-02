namespace McpServer.Api.Models;

public class Incident
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool PlatformIssue { get; set; }
}