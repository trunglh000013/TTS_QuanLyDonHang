using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record GetUserTokenByRefreshTokenRequest
{
    [Required]
    [StringLength(2000)]
    public string RefreshToken { get; set; } = string.Empty;
}