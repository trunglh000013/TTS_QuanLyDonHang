namespace ProductTest.Application.DTOs.Response.RolePermission;

public sealed record GetRolePermissionsByPermissionIdResponse
{
    public List<RolePermissionDto> Items { get; set; } = new();
}

