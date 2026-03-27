using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.UserToken;

public sealed record GetUserTokensByUserIdRequest
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; } = string.Empty;
}