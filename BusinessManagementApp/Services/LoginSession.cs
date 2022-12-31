using Microsoft.Extensions.Logging;

namespace BusinessManagementApp.Services;

/// <summary>
/// Contains authorization-related info of a login session.
/// </summary>
public class LoginSession
{
    private string? accessToken = null;

    public string? AccessToken
    {
        get => accessToken;
        set
        {
            accessToken = value;
            logger.LogInformation("Access token updated: " + accessToken);
        }
    }

    private string? refreshToken = null;

    public string? RefreshToken
    {
        get => refreshToken;
        set
        {
            refreshToken = value;
            logger.LogInformation("Refresh token updated: " + accessToken);
        }
    }

    private readonly ILogger<LoginSession> logger;

    public LoginSession(ILogger<LoginSession> logger)
    {
        this.logger = logger;
    }
}