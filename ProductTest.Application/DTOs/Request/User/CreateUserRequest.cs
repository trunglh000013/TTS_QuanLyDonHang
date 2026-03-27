using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record CreateUserRequest
{
    [Required]
    [StringLength(200)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string PasswordHash { get; set; } = string.Empty;
}

