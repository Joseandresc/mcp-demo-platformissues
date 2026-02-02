namespace McpServer.Api.Configuration;

public class SasOptions
{
    public string SigningKey { get; set; } = string.Empty;
    public int AllowedClockSkewMinutes { get; set; } = 5;
    public string Token { get; set; } = string.Empty;
}