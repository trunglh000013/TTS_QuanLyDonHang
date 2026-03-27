using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;
using ProductTest.Application.Features.v2.Users.Commands.CreateUser;
using ProductTest.Application.Features.v2.Users.Commands.DeleteUser;
using ProductTest.Application.Features.v2.Users.Commands.GrantPermission;
using ProductTest.Application.Features.v2.Users.Commands.GrantRole;
using ProductTest.Application.Features.v2.Users.Commands.RevokePermission;
using ProductTest.Application.Features.v2.Users.Commands.RevokeRole;
using ProductTest.Application.Features.v2.Users.Commands.UpdateUser;
using ProductTest.Application.Features.v2.Users.Queries.GetAllUsers;
using ProductTest.Application.Features.v2.Users.Queries.GetUserByEmail;
using ProductTest.Application.Features.v2.Users.Queries.GetUserById;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/user")]
public sealed class UserController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll([FromBody] GetAllUserRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllUsersQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllUsersResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUserByIdQuery(new GetUserByIdRequest { Id = id }), cancellationToken);
        if (response.User is null) return NotFound();

        return Ok(BaseApiResponse<GetUserByIdResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-email/{email}")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUserByEmailQuery(new GetUserByEmailRequest { Email = email }), cancellationToken);
        if (response.User is null) return NotFound();

        return Ok(BaseApiResponse<GetUserByEmailResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateUserCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserBody request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateUserCommand(new UpdateUserRequest { Id = id, Body = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdateUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteUserCommand(new DeleteUserRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("grant-role/{userId}")]
    public async Task<IActionResult> GrantRole([FromRoute] string userId, [FromRoute] List<string> roleIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GrantRoleCommand(new GrantRoleUserRequest { UserId = userId, RoleIds = roleIds }), cancellationToken);
        return Ok(BaseApiResponse<GrantRoleUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("revoke-role/{userId}")]
    public async Task<IActionResult> RevokeRole([FromRoute] string userId, [FromRoute] List<string> roleIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RevokeRoleCommand(new RevokeRoleUserRequest { UserId = userId, RoleIds = roleIds }), cancellationToken);
        return Ok(BaseApiResponse<RevokeRoleUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("grant-permission/{userId}")]
    public async Task<IActionResult> GrantPermission([FromRoute] string userId, [FromRoute] List<string> permissionIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GrantPermissionCommand(new GrantPermissionUserRequest { UserId = userId, PermissionIds = permissionIds }), cancellationToken);
        return Ok(BaseApiResponse<GrantPermissionUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("revoke-permission/{userId}")]
    public async Task<IActionResult> RevokePermission([FromRoute] string userId, [FromRoute] List<string> permissionIds, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RevokePermissionCommand(new RevokePermissionUserRequest { UserId = userId, PermissionIds = permissionIds }), cancellationToken);
        return Ok(BaseApiResponse<RevokePermissionUserResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }
}