using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Role;

public sealed record GetRoleByCodeRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}

