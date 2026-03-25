using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record FilterProductRequest : PaginationRequest
{
    /// <summary>
    /// Filter by name, supports partial matching.
    /// </summary>
    [StringLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// Filter by minimum product price. (inclusive)
    /// </summary>
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Filter by maximum product price. (inclusive)
    /// </summary>
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Lọc theo tên category (khớp <c>Products.Category</c>, NVARCHAR).
    /// </summary>
    [StringLength(100)]
    public string? Category { get; set; }

    /// <summary>
    /// Indicates whether to only return active products.
    /// </summary>
    public bool? IsActive { get; set; }
}
