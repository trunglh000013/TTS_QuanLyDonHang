namespace ProductTest.Domain.Entities;

public sealed class RolePermission
{
    public string RoleId { get; set; } = string.Empty;
    public string PermissionId { get; set; } = string.Empty;
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Role? Role { get; set; } = new Role();
    public Permission? Permission { get; set; } = new Permission();
}
