using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Permission;

public sealed record GetPermissionByCodeRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}

