using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;
using ProductTest.Application.Features.v2.Customers.Commands.CreateCustomer;
using ProductTest.Application.Features.v2.Customers.Commands.DeleteCustomer;
using ProductTest.Application.Features.v2.Customers.Commands.UpdateCustomer;
using ProductTest.Application.Features.v2.Customers.Queries.GetAllCustomers;
using ProductTest.Application.Features.v2.Customers.Queries.GetCustomerByCode;
using ProductTest.Application.Features.v2.Customers.Queries.GetCustomerById;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/customer")]
public sealed class CustomerController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateCustomerCommand(request), cancellationToken);
        return Ok(BaseApiResponse<CreateCustomerResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCustomerCommand(new DeleteCustomerRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<DeleteCustomerResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCustomerBody request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateCustomerCommand(new UpdateCustomerRequest { Id = id, Body = request }), cancellationToken);
        return Ok(BaseApiResponse<UpdateCustomerResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll(
        [FromBody] GetAllCustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCustomersQuery(request), cancellationToken);
        return Ok(BaseApiResponse<GetAllCustomersResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-code/{code}")]
    public async Task<IActionResult> GetByCode(
        [FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByCodeQuery(new GetCustomerByCodeRequest { Code = code }), cancellationToken);
        return Ok(BaseApiResponse<GetCustomerByCodeResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }

    [HttpPost("get-by-id/{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(new GetCustomerByIdRequest { Id = id }), cancellationToken);
        return Ok(BaseApiResponse<GetCustomerByIdResponse>.SuccessResult(result, localizer["OperationCompletedSuccessfully"]));
    }
}
