namespace ProductTest.Application.DTOs.Response.RolePermission;

public sealed record GetRolePermissionsByRoleIdResponse
{
    public List<RolePermissionDto> Items { get; set; } = new();
}

