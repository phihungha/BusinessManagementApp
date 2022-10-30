using Microsoft.Extensions.Logging;

namespace BusinessManagementApp.Services;

public class LoginSession
{

    private string? _accessToken;
    public string? AccessToken
    {
        get => _accessToken;
        set
        {
            _accessToken = value;
            _logger.LogInformation("Access Token Updated: " + _accessToken);
        }
    }

    private string? _refreshToken;
    public string? RefreshToken
    {
        get => _refreshToken;
        set
        {
            _refreshToken = value;
            _logger.LogInformation("Refresh Token Updated: " + _accessToken);
        }
    }

    private readonly ILogger<LoginSession> _logger;

    public LoginSession(ILogger<LoginSession> logger)
    {
        _logger = logger;
    }

}