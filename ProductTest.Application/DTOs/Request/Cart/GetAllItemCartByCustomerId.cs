using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Cart;

public sealed record GetAllItemCartByCustomerIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;
}
