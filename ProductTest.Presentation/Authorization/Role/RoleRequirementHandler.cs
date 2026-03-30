using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.Features.v2.UserRoles.Queries.GetUserRolesByUserId;

namespace ProductTest.Presentation.Authorization.Role;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoleRequirementHandler> _logger;

    public RoleRequirementHandler(
        IMediator mediator,
        ILogger<RoleRequirementHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        // Get UserId from claims
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
        {
            _logger.LogWarning("User ID claim not found in token");
            return;
        }

        var userRoles = await _mediator.Send(
            new GetUserRolesByUserIdQuery(new GetUserRolesByUserIdRequest { UserId = userIdClaim }),
            CancellationToken.None);

        var userRoleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in userRoles.Items)
        {
            AddIfPresent(userRoleNames, item.RoleName);
        }

        if (userRoleNames.Count == 0)
        {
            _logger.LogWarning("No permissions found for user: {UserId}", userIdClaim);
            return;
        }

        if (userRoleNames.Count == 0)
        {
            _logger.LogWarning("No roles found for user: {UserId}", userIdClaim);
            return;
        }

        var hasRequiredRole = requirement.AllowedRoles.Any(required =>
            userRoleNames.Contains(required));

        if (hasRequiredRole)
        {
            _logger.LogDebug(
                "User {UserId} has required role. Roles: {UserRoles}, Required: {RequiredRoles}",
                userIdClaim, string.Join(", ", userRoleNames), string.Join(", ", requirement.AllowedRoles));
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogWarning(
                "User {UserId} does not have required role. User roles: {UserRoles}, Required: {RequiredRoles}",
                userIdClaim, string.Join(", ", userRoleNames), string.Join(", ", requirement.AllowedRoles));
        }

    }

    private static void AddIfPresent(HashSet<string> set, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            set.Add(value.Trim());
    }
}
