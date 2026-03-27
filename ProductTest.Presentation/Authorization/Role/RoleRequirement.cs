using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProductTest.Presentation.Authorization.Role;

public class RoleRequirement : IAuthorizationRequirement
{
    public RoleRequirement(params string[] allowedRoles)
    {
        AllowedRoles = (allowedRoles ?? Array.Empty<string>()).ToArray();
    }

    public IReadOnlyCollection<string> AllowedRoles { get; }
}