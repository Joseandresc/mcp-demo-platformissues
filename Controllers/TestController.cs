using McpServer.Api.Mcp.Tools;
using Microsoft.AspNetCore.Mvc;

namespace McpServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IncidentTool _incidentTool;
    private readonly ILogger<TestController> _logger;

    public TestController(
        IncidentTool incidentTool,
        ILogger<TestController> logger)
    {
        _incidentTool = incidentTool;
        _logger = logger;
    }

    [HttpGet("incident/{incidentId}")]
    public async Task<IActionResult> GetIncident(string incidentId)
    {
        try
        {
            _logger.LogInformation("Test endpoint called for incident ID: {IncidentId}", incidentId);

            if (string.IsNullOrWhiteSpace(incidentId))
            {
                _logger.LogWarning("Test endpoint called with null or empty incident ID");
                return BadRequest(new { error = "Incident ID cannot be null or empty" });
            }

            var result = await _incidentTool.ExecuteAsync(incidentId);

            _logger.LogInformation("Test endpoint successfully returned incident: {IncidentId}", incidentId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Bad request for incident ID: {IncidentId}", incidentId);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in test endpoint for incident ID: {IncidentId}", incidentId);
            return StatusCode(500, new { error = "An error occurred while processing your request" });
        }
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        try
        {
            _logger.LogDebug("Health check endpoint called");
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in health check endpoint");
            return StatusCode(500, new { status = "unhealthy", error = ex.Message });
        }
    }

    [HttpPost("tools/list")]
    public IActionResult ListTools()
    {
        var tools = new[]
        {
            new { name = "Incident Tool", id = "incident_tool" },
            new { name = "Status Tool", id = "status_tool" }
        };

        return Ok(tools);
    }
}
