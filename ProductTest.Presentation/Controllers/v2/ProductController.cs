using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.Features.v2.Products.Commands.CreateProduct;
using ProductTest.Application.Features.v2.Products.Commands.UpdateProduct;
using ProductTest.Application.Features.v2.Products.Commands.DeleteProduct;
using ProductTest.Application.Features.v2.Products.Queries.GetProductById;
using ProductTest.Application.Features.v2.Products.Queries.GetAllProducts;
using ProductTest.Application.Features.v2.Products.Queries.SearchProducts;
using ProductTest.Application.Features.v2.Products.Queries.FilterProducts;
using ProductTest.Presentation.Resources;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Response.Product;
using ProductTest.Presentation.Authorization.Attributes;
using ProductTest.Application.Features.v2.Products.Queries.GetAllProductExportXlsx;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/product")]
public sealed class ProductController(IMediator mediator, IStringLocalizer<SharedResource> localizer, ILogger<ProductController> logger) : ControllerBase
{
    [HttpPost("create")]
    [AuthorizeRoles("Administrator", "Manager")]
    [AuthorizePermissions("product.create")]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Create product request received for {ProductName} in category {Category}",
            request.Name,
            request.Category);


        var product = await mediator.Send(new CreateProductCommand(request), cancellationToken);

        logger.LogInformation("Create product request completed with product id {ProductId}", product.Success);
        return Ok(BaseApiResponse<CreateProductResponse>.SuccessResult(product, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all")]
    [AuthorizePermissions("product.read")]
    public async Task<IActionResult> GetAll(
        GetAllProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Get all products request received with page number {PageNumber} and page size {PageSize}",
            request.PageNumber,
            request.PageSize);

        var products = await mediator.Send(
            new GetAllProductsQuery(request),
            cancellationToken);

        return Ok(BaseApiResponse<GetAllProductResponse>.SuccessResult(products, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    [AuthorizePermissions("product.read")]
    public async Task<IActionResult> GetById(
        [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get product by id request received for {ProductId}", id);

        var product = await mediator.Send(new GetProductByIdQuery(new GetProductByIdRequest { Id = id }), cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Get product by id request could not find product {ProductId}", id);
            return NotFound(localizer["ProductNotFound"]);
        }

        logger.LogInformation("Get product by id request completed for {ProductId}", id);
        return Ok(BaseApiResponse<GetProductByIdResponse>.SuccessResult(product, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    [AuthorizeRoles("Administrator", "Manager")]
    [AuthorizePermissions("product.update")]
    public async Task<IActionResult> Update(
        [FromRoute] string id,
        [FromBody] UpdateProductBody request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Update product request received for {ProductId}", id);

        var updatedProduct = await mediator.Send(new UpdateProductCommand(new UpdateProductRequest { Id = id, Body = request }), cancellationToken);

        if (updatedProduct is null)
        {
            logger.LogWarning("Update product request could not find product {ProductId}", id);
            return NotFound(localizer["ProductNotFound"]);
        }

        logger.LogInformation("Update product request completed for {ProductId}", id);
        return Ok(BaseApiResponse<UpdateProductResponse>.SuccessResult(updatedProduct, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    [AuthorizeRoles("Administrator", "Manager")]
    [AuthorizePermissions("product.delete")]
    public async Task<IActionResult> Delete(
        [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete product request received for {ProductId}", id);

        var deleted = await mediator.Send(new DeleteProductCommand(new DeleteProductRequest { Id = id }), cancellationToken);

        if (!deleted.Success)
        {
            logger.LogWarning("Delete product request could not find product {ProductId}", id);
            return NotFound(localizer["ProductNotFound"]);
        }

        logger.LogInformation("Delete product request completed for {ProductId}", id);
        return Ok(BaseApiResponse<DeleteProductResponse>.SuccessResult(deleted, localizer["ProductDeleted"]));
    }

    [HttpPost("search")]
    [AuthorizePermissions("product.read")]
    public async Task<IActionResult> Search(
        [FromBody] SearchProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Search products request received with term {SearchTerm}, page number {PageNumber}, page size {PageSize}",
            request.SearchTerm,
            request.PageNumber,
            request.PageSize);

        var products = await mediator.Send(
            new SearchProductsQuery(request),
            cancellationToken);

        return Ok(BaseApiResponse<SearchProductResponse>.SuccessResult(products, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("filter")]
    [AuthorizePermissions("product.read")]
    public async Task<IActionResult> Filter(
        [FromBody] FilterProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Filter products request received with category {Category}, min price {MinPrice}, max price {MaxPrice}, is active {IsActive}, page number {PageNumber}, page size {PageSize}",
            request.Category,
            request.MinPrice,
            request.MaxPrice,
            request.IsActive,
            request.PageNumber,
            request.PageSize);

        if (request.MinPrice.HasValue && request.MaxPrice.HasValue && request.MinPrice > request.MaxPrice)
        {
            logger.LogWarning(
                "Filter products request was rejected because min price {MinPrice} is greater than max price {MaxPrice}",
                request.MinPrice,
                request.MaxPrice);

            return BadRequest(localizer["InvalidPriceRange"]);
        }

        var products = await mediator.Send(
            new FilterProductsQuery(
                request),
            cancellationToken);

        return Ok(BaseApiResponse<FilterProductResponse>.SuccessResult(products, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("export")]
    //[AuthorizePermissions("product.read")]
    public async Task<IActionResult> ExportProduct([FromBody] ExportProductRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Export product request received.");

        var response = await mediator.Send(new ExportProductQuery(new ExportProductRequest()), cancellationToken);
        return Ok(BaseApiResponse<ExportProductResponse>.SuccessResult(
            response,
            localizer["OperationCompletedSuccessfully"]
        ));
    }
}
