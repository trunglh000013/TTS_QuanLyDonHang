namespace ProductTest.Application.DTOs.Response.Permission;

public sealed record GetPermissionByCodeResponse
{
    public PermissionDto? Permission { get; set; }
}

