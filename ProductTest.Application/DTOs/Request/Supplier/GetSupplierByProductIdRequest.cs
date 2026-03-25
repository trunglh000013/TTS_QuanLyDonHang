using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record GetSupplierByProductIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;
}
