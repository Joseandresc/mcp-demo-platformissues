using McpServer.Api.Configuration;
using McpServer.Api.Mcp;
using McpServer.Api.Middleware;
using McpServer.Api.Security;
using McpServer.Api.Services;
using McpServer.Api.Services.Interfaces;
using McpServer.Api.Mcp.Tools;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}
else
{
    builder.Logging.SetMinimumLevel(LogLevel.Information);
}

builder.Services.Configure<SasOptions>(
    builder.Configuration.GetSection("Sas"));

builder.Services.AddSingleton<SasTokenValidator>();
builder.Services.AddSingleton<IIncidentService, IncidentService>();

// Register IncidentTool for direct testing
builder.Services.AddScoped<IncidentTool>();

// Add controllers for testing
builder.Services.AddControllers();

// Only call AddMcp once - ToolRegistration handles it with tool configuration
ToolRegistration.Register(builder.Services);

var app = builder.Build();

// Add global exception handler (must be first)
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseMiddleware<SasAuthenticationMiddleware>();

// Map controllers for testing
app.MapControllers();

app.MapMcp();

app.Run();