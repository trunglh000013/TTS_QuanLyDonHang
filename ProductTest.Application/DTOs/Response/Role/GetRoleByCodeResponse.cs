namespace ProductTest.Application.DTOs.Response.Role;

public sealed record GetRoleByCodeResponse
{
    public RoleDto? Role { get; set; }
}

