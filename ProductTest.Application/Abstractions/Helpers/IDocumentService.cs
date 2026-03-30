using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ProductTest.Application.DTOs.Request.Document;

namespace ProductTest.Application.Abstractions.DocumentAbstractions;

public interface IDocumentConversionService
{
    Task<string> GenerateDocxAsync(GenerateDocxRequest request, CancellationToken cancellationToken);
}

