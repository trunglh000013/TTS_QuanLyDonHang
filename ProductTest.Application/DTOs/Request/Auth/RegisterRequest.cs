using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Auth;

public sealed record RegisterRequest
{
    [Required]
    [StringLength(64)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    public List<string> RoleIds { get; set; } = new();
}
