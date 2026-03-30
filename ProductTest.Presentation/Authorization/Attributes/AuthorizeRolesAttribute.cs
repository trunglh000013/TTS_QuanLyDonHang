using Microsoft.AspNetCore.Authorization;

namespace ProductTest.Presentation.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        ArgumentNullException.ThrowIfNull(roles);
        Policy = $"roles:{string.Join(",", roles)}";
    }
}
