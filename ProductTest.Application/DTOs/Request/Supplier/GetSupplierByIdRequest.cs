using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record GetSupplierByIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
