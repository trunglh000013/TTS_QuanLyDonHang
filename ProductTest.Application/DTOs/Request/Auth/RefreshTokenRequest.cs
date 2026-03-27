using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Auth;

public sealed record RefreshTokenRequest
{
    [Required]
    [StringLength(2000)]
    public string RefreshToken { get; set; } = string.Empty;
}
