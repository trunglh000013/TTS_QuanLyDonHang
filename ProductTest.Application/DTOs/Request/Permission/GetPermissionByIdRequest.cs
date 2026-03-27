using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Permission;

public sealed record GetPermissionByIdRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}

