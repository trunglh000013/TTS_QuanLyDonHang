using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProductTest.Application.Abstractions.DocumentAbstractions;
using ProductTest.Application.DTOs.Request.Document;
using ProductTest.Application.DTOs.Response.Document;

namespace ProductTest.Application.Features.v1.Document.Commands
{
    /// <summary>
    /// Command to trigger DOCX document generation.
    /// </summary>
    public sealed record GenerateDocxCommand(GenerateDocxRequest Request) : IRequest<GenerateDocxResponse>;

    /// <summary>
    /// Handler for <see cref="GenerateDocxCommand"/> that uses <see cref="IDocumentConversionService"/> to generate DOCX.
    /// </summary>
    public sealed class GenerateDocxCommandHandler : IRequestHandler<GenerateDocxCommand, GenerateDocxResponse>
    {
        private readonly IDocumentConversionService _documentConversionService;

        public GenerateDocxCommandHandler(IDocumentConversionService documentConversionService)
        {
            _documentConversionService = documentConversionService;
        }

        public async Task<GenerateDocxResponse> Handle(GenerateDocxCommand request, CancellationToken cancellationToken)
        {
            var docxPath = await _documentConversionService.GenerateDocxAsync(request.Request, cancellationToken);
            return new GenerateDocxResponse { DocxPath = docxPath };
        }
    }
}