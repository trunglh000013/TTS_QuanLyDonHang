namespace ProductTest.Domain.Entities;

public sealed class UserPermission
{
    public string UserId { get; set; } = string.Empty;
    public string PermissionId { get; set; } = string.Empty;
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public User? User { get; set; } = new User();
    public Permission? Permission { get; set; } = new Permission();
}
