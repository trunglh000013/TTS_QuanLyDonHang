using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.UserRole;

public sealed record GetUserRolesByRoleIdRequest
{
    [Required]
    [StringLength(64)]
    public string RoleId { get; set; } = string.Empty;
}
