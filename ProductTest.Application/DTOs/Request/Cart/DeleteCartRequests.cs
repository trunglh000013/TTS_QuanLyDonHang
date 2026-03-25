using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Cart;

public sealed record DeleteCartRequest
{
    [Required]
    [StringLength(64)]
    public string CartId { get; set; } = string.Empty;
}
