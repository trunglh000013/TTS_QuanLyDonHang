using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Role;

public sealed record UpdateRoleRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateRoleBody? Body { get; set; }
}

public sealed record UpdateRoleBody
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}

