using McpServer.Api.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace McpServer.Api.Security;

public class SasTokenValidator
{
    private readonly SasOptions _options;
    private readonly ILogger<SasTokenValidator> _logger;

    public SasTokenValidator(
        IOptions<SasOptions> options,
        ILogger<SasTokenValidator> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public bool Validate(string sas)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(sas))
            {
                _logger.LogWarning("SAS token validation failed: Token is null or empty");
                return false;
            }

            var query = QueryHelpers.ParseQuery(sas);
            var isValid = string.Equals(sas, _options.Token, StringComparison.Ordinal);

            if (isValid)
            {
                _logger.LogInformation("SAS token validation successful");
            }
            else
            {
                _logger.LogWarning("SAS token validation failed: Token mismatch");
            }

            return isValid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during SAS token validation");
            return false;
        }
    }
}