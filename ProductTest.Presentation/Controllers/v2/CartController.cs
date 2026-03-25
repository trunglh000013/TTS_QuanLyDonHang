using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.Features.v2.Carts.Commands.CreateCart;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.Features.v2.Carts.Commands.AddItemCart;
using ProductTest.Application.Features.v2.Carts.Commands.DeleteItemCart;
using ProductTest.Application.Features.v2.Carts.Commands.DeleteCart;
using ProductTest.Application.Features.v2.Carts.Queries.GetAllItemByCustomerIdCart;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Response.Cart;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/cart")]
public sealed class CartController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateCartCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateCartResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteCart([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCartCommand(new DeleteCartRequest { CartId = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteCartResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("add-item/{id}")]
    public async Task<IActionResult> AddItemCart([FromRoute] string id, [FromBody] AddItemCartBody request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AddItemCartCommand(new AddItemCartRequest { CartId = id, AddItemCartBody = request }), cancellationToken);
        return Ok(BaseApiResponse<AddItemCartResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete-item/{id}")]
    public async Task<IActionResult> DeleteItemCart([FromRoute] string id, [FromBody] DeleteItemCartRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteItemCartCommand(new DeleteItemCartRequest { CartId = id, ProductId = request.ProductId }), cancellationToken);
        return Ok(BaseApiResponse<DeleteItemCartResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all-items-by-customer/{customerId}")]
    public async Task<IActionResult> GetAllItemsByCustomerId([FromRoute] string customerId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllItemByCustomerIdCartQuery(new GetAllItemCartByCustomerIdRequest { CustomerId = customerId }), cancellationToken);
        return Ok(BaseApiResponse<GetAllItemByCustomerIdCartResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }
}

