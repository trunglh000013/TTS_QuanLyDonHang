using Microsoft.AspNetCore.Authorization;

namespace ProductTest.Presentation.Authorization.Permission;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(params string[] allowedPermissions)
    {
        AllowedPermissions = (allowedPermissions ?? Array.Empty<string>()).ToArray();
    }

    public IReadOnlyCollection<string> AllowedPermissions { get; }
}