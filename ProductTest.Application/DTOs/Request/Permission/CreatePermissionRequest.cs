using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Permission;

public sealed record CreatePermissionRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}

