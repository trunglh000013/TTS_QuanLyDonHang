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

        var response = await _mediator.Send(
            new GetUserRolesByUserIdQuery(new GetUserRolesByUserIdRequest { UserId = userIdClaim }),
            CancellationToken.None);

        var userRoleCodes = response.Items
            .Select(x => x.RoleCode)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (userRoleCodes.Count == 0)
        {
            _logger.LogWarning("No roles found for user: {UserId}", userIdClaim);
            return;
        }

        // Check if user has any of the required roles
        var hasRequiredRole = requirement.AllowedRoles.Any(allowedRole =>
            userRoleCodes.Contains(allowedRole, StringComparer.OrdinalIgnoreCase));

        if (hasRequiredRole)
        {
            _logger.LogDebug("User {UserId} has required role. Roles: {UserRoles}, Required: {RequiredRoles}",
                userIdClaim, string.Join(", ", userRoleCodes), string.Join(", ", requirement.AllowedRoles));
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogWarning("User {UserId} does not have required role. User roles: {UserRoles}, Required: {RequiredRoles}",
                userIdClaim, string.Join(", ", userRoleCodes), string.Join(", ", requirement.AllowedRoles));
        }
    }
}
