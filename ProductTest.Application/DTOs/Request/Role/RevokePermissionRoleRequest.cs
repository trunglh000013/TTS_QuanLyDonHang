using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Role;

public sealed record RevokePermissionRoleRequest
{
    [Required]
    [StringLength(64)]
    public string RoleId { get; set; } = string.Empty;

    [Required]
    public List<string> PermissionIds { get; set; } = new();
}