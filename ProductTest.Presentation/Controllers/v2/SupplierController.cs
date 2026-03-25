using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Features.v2.Suppliers.Commands.CreateSupplier;
using ProductTest.Application.Features.v2.Suppliers.Commands.DeleteSupplier;
using ProductTest.Application.Features.v2.Suppliers.Commands.UpdateSupplier;
using ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierByCode;
using ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierById;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.Features.v2.Suppliers.Queries.GetAllSupplier;
using ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierByProductId;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Response.Supplier;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/supplier")]
public sealed class SupplierController(IMediator mediator, IStringLocalizer<SharedResource> localizer, ILogger<SupplierController> logger) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create supplier request received for {SupplierName}", request.Name);
        var result = await mediator.Send(new CreateSupplierCommand(request), cancellationToken);
        logger.LogInformation("Create supplier request completed");
        return Ok(BaseApiResponse<CreateSupplierResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete supplier request received for {SupplierId}", id);
        var result = await mediator.Send(new DeleteSupplierCommand(new DeleteSupplierRequest { Id = id }), cancellationToken);
        logger.LogInformation("Delete supplier request completed");
        return Ok(BaseApiResponse<DeleteSupplierResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateSupplierBody request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update supplier request received for {SupplierId}", id);
        var result = await mediator.Send(new UpdateSupplierCommand(new UpdateSupplierRequest { Id = id, UpdateSupplierBody = request }), cancellationToken);
        logger.LogInformation("Update supplier request completed");
        return Ok(BaseApiResponse<UpdateSupplierResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll(
        [FromBody] GetAllSupplierRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all suppliers request received with page number {PageNumber} and page size {PageSize}", request.PageNumber, request.PageSize);
        var result = await mediator.Send(new GetAllSupplierQuery(request), cancellationToken);
        logger.LogInformation("Get all suppliers request completed");
        return Ok(BaseApiResponse<GetAllSupplierResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    public async Task<IActionResult> GetByCode(
        [FromRoute] string code, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get supplier by code request received for {SupplierCode}", code);
        var result = await mediator.Send(new GetSupplierByCodeQuery(new GetSupplierByCodeRequest { Code = code }), cancellationToken);
        logger.LogInformation("Get supplier by code request completed");
        return Ok(BaseApiResponse<GetSupplierByCodeResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get supplier by id request received for {SupplierId}", id);
        var result = await mediator.Send(new GetSupplierByIdQuery(new GetSupplierByIdRequest { Id = id }), cancellationToken);
        logger.LogInformation("Get supplier by id request completed");
        return Ok(BaseApiResponse<GetSupplierByIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-product-id/{productId}")]
    public async Task<IActionResult> GetByProductId(
        [FromRoute] string productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get supplier by product id request received for {ProductId}", productId);
        var result = await mediator.Send(new GetSupplierByProductIdQuery(new GetSupplierByProductIdRequest { ProductId = productId }), cancellationToken);
        logger.LogInformation("Get supplier by product id request completed");
        return Ok(BaseApiResponse<GetSupplierByProductIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }
}
