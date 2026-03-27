namespace ProductTest.Application.DTOs.Response.Role;

public sealed record GetRoleByIdResponse
{
    public RoleDto? Role { get; set; }
}

