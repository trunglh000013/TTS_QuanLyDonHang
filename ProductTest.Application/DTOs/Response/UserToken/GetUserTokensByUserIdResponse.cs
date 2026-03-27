using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs.Response.UserToken;

namespace ProductTest.Application.DTOs.Request.UserToken;

public sealed record GetUserTokensByUserIdResponse
{
    public List<UserTokenDto> Items { get; set; } = new();
}