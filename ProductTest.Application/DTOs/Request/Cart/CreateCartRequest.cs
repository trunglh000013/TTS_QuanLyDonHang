using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Cart;

public sealed record CreateCartRequest
{
    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;
}
