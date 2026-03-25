using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record GetSupplierByCodeRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}
