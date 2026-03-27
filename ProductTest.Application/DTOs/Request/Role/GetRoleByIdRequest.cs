using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Role;

public sealed record GetRoleByIdRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}

