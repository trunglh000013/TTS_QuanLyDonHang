using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record GetUserByEmailRequest
{
    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;
}

