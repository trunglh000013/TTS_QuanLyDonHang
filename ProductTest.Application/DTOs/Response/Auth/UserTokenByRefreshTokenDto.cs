namespace ProductTest.Application.DTOs.Response.Auth;

public sealed record UserTokenByRefreshTokenDto
{
    public string UserId { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; }
}

