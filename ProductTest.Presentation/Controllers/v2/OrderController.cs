using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.Features.v2.Orders.Commands.CreateOrder;
using ProductTest.Application.Features.v2.Orders.Queries.GetOrderById;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.Features.v2.Orders.Commands.UpdateOrder;
using ProductTest.Application.Features.v2.Orders.Commands.DeleteOrder;
using ProductTest.Application.Features.v2.Orders.Queries.GetAllOrder;
using ProductTest.Application.Features.v2.Orders.Queries.GetOrderByCode;
using ProductTest.Application.Features.v2.Orders.Queries.GetOrderByCustomerId;
using ProductTest.Application.Features.v2.Orders.Queries.GetOrderDetail;
using ProductTest.Application.Features.v2.Orders.Commands.CreateOrderByCartId;
using ProductTest.Application.DTOs.Response.Order;
using ProductTest.Application.DTOs;
using ProductTest.Presentation.Authorization.Attributes;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Authorize]
[Route("api/v{version:apiVersion}/order")]
public sealed class OrderController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("create")]
    [AuthorizePermissions("order.create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateOrderCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateOrderResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("create-by-cart-id/{cartId}")]
    [AuthorizePermissions("order.create")]
    public async Task<IActionResult> CreateByCartId([FromRoute] string cartId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateOrderByCartIdCommand(new CreateOrderByCartIdRequest { CartId = cartId }), cancellationToken);
        return Ok(BaseApiResponse<CreateOrderByCartIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    [AuthorizePermissions("order.delete")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(new DeleteOrderRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteOrderResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    [AuthorizePermissions("order.update")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateOrderBody request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateOrderCommand(new UpdateOrderRequest { Id = id, UpdateOrderBody = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdateOrderResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all")]
    [AuthorizePermissions("order.read")]
    public async Task<IActionResult> GetAll(
        [FromBody] GetAllOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllOrderQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllOrderResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    [AuthorizePermissions("order.read")]
    public async Task<IActionResult> GetByCode(
        [FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByCodeQuery(new GetOrderByCodeRequest { Code = code }), cancellationToken);
        return Ok(BaseApiResponse<GetOrderByCodeResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    [AuthorizePermissions("order.read")]
    public async Task<IActionResult> GetById(
        [FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(new GetOrderByIdRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<GetOrderByIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-customer-id/{customerId}")]
    [AuthorizePermissions("order.read")]
    public async Task<IActionResult> GetByCustomerId(
        [FromRoute] string customerId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByCustomerIdQuery(new GetOrderByCustomerIdRequest { CustomerId = customerId }), cancellationToken);
        return Ok(BaseApiResponse<GetOrderByCustomerIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-detail/{orderId}")]
    [AuthorizePermissions("order.read")]
    public async Task<IActionResult> GetDetail(
        [FromRoute] string orderId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderDetailQuery(new GetOrderDetailRequest { OrderId = orderId }), cancellationToken);
        return Ok(BaseApiResponse<GetOrderDetailResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }
}
