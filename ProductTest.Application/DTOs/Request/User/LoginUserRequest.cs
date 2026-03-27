using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record LoginUserRequest
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime IssuedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
