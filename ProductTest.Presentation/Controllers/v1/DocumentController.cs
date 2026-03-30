using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Localization;
using ProductTest.Presentation.Resources;
using ProductTest.Application.Features.v1.Document.Commands;
using ProductTest.Application.DTOs.Request.Document;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Response.Document;

namespace ProductTest.Presentation.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/document")]
    public sealed class DocumentController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
    {
        [HttpPost("generate-docx")]
        public async Task<IActionResult> GenerateDocx([FromBody] GenerateDocxRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GenerateDocxCommand(request), cancellationToken);
            return Ok(BaseApiResponse<GenerateDocxResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
        }
    }
}