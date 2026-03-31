using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Report;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/report")]
    public class ReportController(IMemoryCache memoryCache, IStringLocalizer<SharedResource> localizer, ILogger<ReportController> logger) : ControllerBase
    {
        [HttpPost("download/{key}")]
        public IActionResult Download(string key, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key is required");

            logger.LogInformation("Downloading file with key: {Key}", key);

            logger.LogInformation("Cache instance export: {Hash}", memoryCache.GetHashCode());

            if (!memoryCache.TryGetValue(key, out CachedFile file))
            {
                logger.LogInformation("File found in cache: {FileName}, {FileSize}", file.FileName, file.Content.Length);
                return BadRequest("File not found");
            }

            logger.LogInformation("File found in cache: {FileName}, {FileSize}", file.FileName, file.Content.Length);
            return File(file.Content, file.ContentType, file.FileName);
        }
    }
}