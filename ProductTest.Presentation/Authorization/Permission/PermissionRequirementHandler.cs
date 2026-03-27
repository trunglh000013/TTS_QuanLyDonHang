using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ProductTest.Application.DTOs.Request.UserPermission;
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
        // Get UserId from claims
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
        {
            _logger.LogWarning("User ID claim not found in token");
            return;
        }

        var response = await _mediator.Send(
            new GetUserPermissionsByUserIdQuery(new GetUserPermissionsByUserIdRequest { UserId = userIdClaim }),
            CancellationToken.None);

        var userPermissionCodes = response.Items
            .Select(x => x.PermissionCode)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (userPermissionCodes.Count == 0)
        {
            _logger.LogWarning("No permissions found for user: {UserId}", userIdClaim);
            return;
        }

        // Check if user has any of the required permissions
        var hasRequiredPermission = requirement.AllowedPermissions.Any(allowedPermission =>
            userPermissionCodes.Contains(allowedPermission, StringComparer.OrdinalIgnoreCase));

        if (hasRequiredPermission)
        {
            _logger.LogDebug("User {UserId} has required permission. Permissions: {UserPermissions}, Required: {RequiredPermissions}",
                userIdClaim, string.Join(", ", userPermissionCodes), string.Join(", ", requirement.AllowedPermissions));
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogWarning("User {UserId} does not have required permission. User permissions: {UserPermissions}, Required: {RequiredPermissions}",
                userIdClaim, string.Join(", ", userPermissionCodes), string.Join(", ", requirement.AllowedPermissions));
        }
    }
}
