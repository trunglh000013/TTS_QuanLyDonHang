using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Role;

public sealed record CreateRoleRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}

