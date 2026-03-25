using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record CreateOrderByCartIdRequest
{
    [Required]
    [StringLength(64)]
    public string CartId { get; set; } = string.Empty;
}
