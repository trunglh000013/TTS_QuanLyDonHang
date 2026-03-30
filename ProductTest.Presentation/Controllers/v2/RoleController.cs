using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;
using ProductTest.Application.Features.v2.Roles.Commands.CreateRole;
using ProductTest.Application.Features.v2.Roles.Commands.DeleteRole;
using ProductTest.Application.Features.v2.Roles.Commands.GrantPermission;
using ProductTest.Application.Features.v2.Roles.Commands.RevokePermission;
using ProductTest.Application.Features.v2.Roles.Commands.UpdateRole;
using ProductTest.Application.Features.v2.Roles.Queries.GetAllRoles;
using ProductTest.Application.Features.v2.Roles.Queries.GetRoleByCode;
using ProductTest.Application.Features.v2.Roles.Queries.GetRoleById;
using ProductTest.Presentation.Authorization.Attributes;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Authorize]
[AuthorizeRoles("Administrator")]
[Route("api/v{version:apiVersion}/role")]
public sealed class RoleController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("get-all")]
    [AuthorizePermissions("role.read")]
    public async Task<IActionResult> GetAll([FromBody] GetAllRoleRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllRolesQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllRolesResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    [AuthorizePermissions("role.read")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetRoleByIdQuery(new GetRoleByIdRequest { Id = id }), cancellationToken);
        if (response.Role is null) return NotFound();

        return Ok(BaseApiResponse<GetRoleByIdResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    [AuthorizePermissions("role.read")]
    public async Task<IActionResult> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetRoleByCodeQuery(new GetRoleByCodeRequest { Code = code }), cancellationToken);
        if (response.Role is null) return NotFound();

        return Ok(BaseApiResponse<GetRoleByCodeResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("create")]
    [AuthorizePermissions("role.create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateRoleCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateRoleResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    [AuthorizePermissions("role.update")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateRoleBody request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateRoleCommand(new UpdateRoleRequest { Id = id, Body = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdateRoleResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    [AuthorizePermissions("role.delete")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteRoleCommand(new DeleteRoleRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteRoleResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("grant-permission/{roleId}")]
    [AuthorizePermissions("role.grantPermission")]
    public async Task<IActionResult> GrantPermission([FromRoute] string roleId, [FromRoute] List<string> permissionIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GrantPermissionCommand(new GrantPermissionRoleRequest { RoleId = roleId, PermissionIds = permissionIds }), cancellationToken);
        return Ok(BaseApiResponse<GrantPermissionRoleResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("revoke-permission/{roleId}")]
    [AuthorizePermissions("role.revokePermission")]
    public async Task<IActionResult> RevokePermission([FromRoute] string roleId, [FromRoute] List<string> permissionIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RevokePermissionCommand(new RevokePermissionRoleRequest { RoleId = roleId, PermissionIds = permissionIds }), cancellationToken);
        return Ok(BaseApiResponse<RevokePermissionRoleResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }
}
