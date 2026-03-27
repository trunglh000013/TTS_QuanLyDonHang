using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;
using ProductTest.Application.Features.v2.Permissions.Commands.CreatePermission;
using ProductTest.Application.Features.v2.Permissions.Commands.DeletePermission;
using ProductTest.Application.Features.v2.Permissions.Commands.UpdatePermission;
using ProductTest.Application.Features.v2.Permissions.Queries.GetAllPermissions;
using ProductTest.Application.Features.v2.Permissions.Queries.GetPermissionByCode;
using ProductTest.Application.Features.v2.Permissions.Queries.GetPermissionById;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/permission")]
public sealed class PermissionController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll([FromBody] GetAllPermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllPermissionsQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllPermissionsResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetPermissionByIdQuery(new GetPermissionByIdRequest { Id = id }), cancellationToken);
        if (response.Permission is null) return NotFound();

        return Ok(BaseApiResponse<GetPermissionByIdResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetPermissionByCodeQuery(new GetPermissionByCodeRequest { Code = code }), cancellationToken);
        if (response.Permission is null) return NotFound();

        return Ok(BaseApiResponse<GetPermissionByCodeResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreatePermissionCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreatePermissionResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatePermissionBody request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdatePermissionCommand(new UpdatePermissionRequest { Id = id, Body = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdatePermissionResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeletePermissionCommand(new DeletePermissionRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeletePermissionResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
    }
}