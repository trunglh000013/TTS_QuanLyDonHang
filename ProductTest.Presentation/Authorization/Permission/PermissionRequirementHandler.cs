using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ProductTest.Application.DTOs.Request.RolePermission;
using ProductTest.Application.DTOs.Request.UserPermission;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.Features.v2.RolePermissions.Queries.GetRolePermissionsByRoleId;
using ProductTest.Application.Features.v2.UserRoles.Queries.GetUserRolesByUserId;
using ProductTest.Application.Features.v2.UserPermissions.Queries.GetUserPermissionsByUserId;

namespace ProductTest.Presentation.Authorization.Permission;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IMediator _mediator;
    private readonly ILogger<PermissionRequirementHandler> _logger;

    public PermissionRequirementHandler(
        IMediator mediator,
        ILogger<PermissionRequirementHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
        {
            _logger.LogWarning("User ID claim not found in token");
            return;
        }

        var direct = await _mediator.Send(
            new GetUserPermissionsByUserIdQuery(new GetUserPermissionsByUserIdRequest { UserId = userIdClaim }),
            CancellationToken.None);

        var permissionName = direct.Items.FirstOrDefault()?.PermissionName;
        _logger.LogInformation("User {UserId} has permissions: {PermissionName}", userIdClaim, permissionName);

        var identifiers = new HashSet<string>(direct.Items.Select(item => item.PermissionName));
        _logger.LogInformation("User {UserId} has permissions: {PermissionName}", userIdClaim, string.Join(", ", identifiers));

        if (identifiers.Count == 0)
        {
            _logger.LogWarning("No permissions found for user: {UserId}", userIdClaim);
            return;
        }

        var hasRequiredPermission = requirement.AllowedPermissions.Any(required =>
            identifiers.Contains(required));

        if (hasRequiredPermission)
        {
            _logger.LogDebug(
                "User {UserId} has required permission. Permissions: {UserPermissions}, Required: {RequiredPermissions}",
                userIdClaim, string.Join(", ", identifiers), string.Join(", ", requirement.AllowedPermissions));
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogWarning(
                "User {UserId} does not have required permission. User permissions: {UserPermissions}, Required: {RequiredPermissions}",
                userIdClaim, string.Join(", ", identifiers), string.Join(", ", requirement.AllowedPermissions));
        }
    }

    private static void AddIfPresent(HashSet<string> set, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            set.Add(value.Trim());
    }
}
