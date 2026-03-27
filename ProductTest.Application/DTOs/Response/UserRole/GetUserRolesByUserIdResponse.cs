namespace ProductTest.Application.DTOs.Response.UserRole;

public sealed record GetUserRolesByUserIdResponse
{
    public List<UserRoleDto> Items { get; set; } = new();
}