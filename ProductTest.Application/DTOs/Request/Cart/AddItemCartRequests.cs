using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Cart;

public sealed record AddItemCartRequest
{
    [Required]
    [StringLength(64)]
    public string CartId { get; set; } = string.Empty;

    [Required]
    public AddItemCartBody? AddItemCartBody { get; set; }
}

public sealed record AddItemCartBody
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
