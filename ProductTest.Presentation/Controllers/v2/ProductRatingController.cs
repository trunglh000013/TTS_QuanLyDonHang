using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.Features.v2.ProductRatings.Commands.CreateProductRating;
using ProductTest.Application.Features.v2.ProductRatings.Commands.DeleteProductRating;
using ProductTest.Application.Features.v2.ProductRatings.Commands.UpdateProductRating;
using ProductTest.Application.Features.v2.ProductRatings.Queries.GetAllProductRating;
using ProductTest.Application.Features.v2.ProductRatings.Queries.GetProductRatingByCode;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.Features.v2.ProductRatings.Queries.GetProductRatingByProductId;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Response.ProductRating;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/product-rating")]
public sealed class ProductRatingController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductRatingRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateProductRatingCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateProductRatingResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductRatingCommand(new DeleteProductRatingRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteProductRatingResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateProductRatingBody request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateProductRatingCommand(new UpdateProductRatingRequest { Id = id, Body = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdateProductRatingResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll(
        [FromBody] GetAllProductRatingRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductRatingQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllProductRatingResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    public async Task<IActionResult> GetByCode(
        [FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductRatingByCodeQuery(new GetProductRatingByCodeRequest { Code = code }), cancellationToken);
        return Ok(BaseApiResponse<GetProductRatingByCodeResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductRatingByIdQuery(new GetProductRatingByIdRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<GetProductRatingByIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-product-id/{productId}")]
    public async Task<IActionResult> GetByProductId(
        [FromRoute] string productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductRatingByProductIdQuery(new GetProductRatingByProductIdRequest { ProductId = productId }), cancellationToken);
        return Ok(BaseApiResponse<GetProductRatingByProductIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }
}

