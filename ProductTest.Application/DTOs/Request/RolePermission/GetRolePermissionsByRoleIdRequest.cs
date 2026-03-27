using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.RolePermission;

public sealed record GetRolePermissionsByRoleIdRequest
{
    [Required]
    [StringLength(64)]
    public string RoleId { get; set; } = string.Empty;
}
