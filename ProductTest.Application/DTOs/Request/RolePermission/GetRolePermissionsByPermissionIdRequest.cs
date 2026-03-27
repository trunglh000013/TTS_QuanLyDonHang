using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.RolePermission;

public sealed record GetRolePermissionsByPermissionIdRequest
{
    [Required]
    [StringLength(64)]
    public string PermissionId { get; set; } = string.Empty;
}
