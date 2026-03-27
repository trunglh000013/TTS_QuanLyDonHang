namespace ProductTest.Application.DTOs.Response.Auth;

public sealed record RefreshTokenResponse
{
    public string AccessToken { get; init; } = string.Empty;
}
