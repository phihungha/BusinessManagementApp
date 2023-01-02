using Microsoft.Extensions.Logging;

namespace BusinessManagementApp.Services;

/// <summary>
/// Contains authorization-related data of a user session.
/// </summary>
public class SessionAuthData
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
            logger.LogInformation("Refresh token updated: " + refreshToken);
        }
    }

    private readonly ILogger<SessionAuthData> logger;

    public SessionAuthData(ILogger<SessionAuthData> logger)
    {
        this.logger = logger;
    }
}