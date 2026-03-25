using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs;

public record PaginationRequest
{
    private const int DefaultPageSize = 10;
    private const int MaxPageSize = 100;

    private int _pageSize = DefaultPageSize;
    private int _pageNumber = 1;

    /// <summary>
    /// Page number (1-based).
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be at least 1.")]
    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < 1 ? 1 : value;
    }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value < 1 ? DefaultPageSize : (value > MaxPageSize ? MaxPageSize : value);
    }
}