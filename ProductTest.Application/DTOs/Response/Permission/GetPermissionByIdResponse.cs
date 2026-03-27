namespace ProductTest.Application.DTOs.Response.Permission;

public sealed record GetPermissionByIdResponse
{
    public PermissionDto? Permission { get; set; }
}

