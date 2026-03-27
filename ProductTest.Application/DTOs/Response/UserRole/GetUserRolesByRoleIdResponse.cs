namespace ProductTest.Application.DTOs.Response.UserRole;

public sealed record GetUserRolesByRoleIdResponse
{
    public List<UserRoleDto> Items { get; set; } = new();
}

