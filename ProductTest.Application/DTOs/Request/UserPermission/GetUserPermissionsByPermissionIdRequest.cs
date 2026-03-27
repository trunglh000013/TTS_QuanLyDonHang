using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.UserPermission;

public sealed record GetUserPermissionsByPermissionIdRequest
{
    [Required]
    [StringLength(64)]
    public string PermissionId { get; set; } = string.Empty;
}
