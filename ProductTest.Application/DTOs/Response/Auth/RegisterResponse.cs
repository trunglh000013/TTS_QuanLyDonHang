namespace ProductTest.Application.DTOs.Response.Auth;

public sealed record RegisterResponse
{
    public bool Success { get; init; }
    public string? UserId { get; init; }
    public string? Email { get; init; }
}
