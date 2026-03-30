using Microsoft.AspNetCore.Authorization;

namespace ProductTest.Presentation.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class AuthorizePermissionsAttribute : AuthorizeAttribute
{
    public AuthorizePermissionsAttribute(params string[] permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);
        Policy = $"permissions:{string.Join(",", permissions)}";
    }
}
