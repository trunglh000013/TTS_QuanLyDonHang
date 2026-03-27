using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.UserRole;

public sealed record GetUserRolesByUserIdRequest
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; } = string.Empty;
}
