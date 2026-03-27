using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Permission;

public sealed record UpdatePermissionRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdatePermissionBody? Body { get; set; }
}

public sealed record UpdatePermissionBody
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}

