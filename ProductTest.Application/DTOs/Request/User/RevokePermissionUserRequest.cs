using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.User;

public sealed record RevokePermissionUserRequest
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public List<string> PermissionIds { get; set; } = new();
}
