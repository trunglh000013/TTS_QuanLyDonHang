using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.UserPermission;

public sealed record GetUserPermissionsByUserIdRequest
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; } = string.Empty;
}
