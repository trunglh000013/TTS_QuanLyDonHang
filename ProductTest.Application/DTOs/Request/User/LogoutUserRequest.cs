using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record LogoutUserRequest
{
    [Required]
    [StringLength(2000)]
    public string RefreshToken { get; set; } = string.Empty;
}
