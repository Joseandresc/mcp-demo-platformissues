namespace McpServer.Api.Exceptions;

public class IncidentNotFoundException : Exception
{
    public string IncidentId { get; }

    public IncidentNotFoundException(string incidentId)
        : base($"Incident with ID '{incidentId}' was not found.")
    {
        IncidentId = incidentId;
    }

    public IncidentNotFoundException(string incidentId, string message)
        : base(message)
    {
        IncidentId = incidentId;
    }

    public IncidentNotFoundException(string incidentId, string message, Exception innerException)
        : base(message, innerException)
    {
        IncidentId = incidentId;
    }
}
