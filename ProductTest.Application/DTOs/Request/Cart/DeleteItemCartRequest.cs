using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Cart;

public sealed record DeleteItemCartRequest
{
    [Required]
    [StringLength(64)]
    public string CartId { get; set; } = string.Empty;

    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;
}
