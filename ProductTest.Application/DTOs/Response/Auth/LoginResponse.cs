using System.Collections.Generic;

namespace ProductTest.Application.DTOs.Response.Auth;

public sealed record LoginResponse
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public DateTime RefreshTokenExpiry { get; init; } = DateTime.UtcNow;

    public string UserId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Username { get; init; }

    public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
}
