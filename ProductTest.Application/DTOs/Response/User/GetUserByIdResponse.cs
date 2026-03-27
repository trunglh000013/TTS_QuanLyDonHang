namespace ProductTest.Application.DTOs.Response.User;

public sealed record GetUserByIdResponse
{
    public UserDto? User { get; set; }
}

