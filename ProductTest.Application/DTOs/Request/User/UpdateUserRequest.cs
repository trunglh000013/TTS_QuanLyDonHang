using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record UpdateUserRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateUserBody? Body { get; set; }
}

public sealed record UpdateUserBody
{
    [StringLength(200)]
    public string Username { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [StringLength(500)]
    public string PasswordHash { get; set; } = string.Empty;
}

