namespace ProductTest.Application.DTOs.Response.User;

public sealed record GetUserByEmailResponse
{
    public UserDto? User { get; set; }
}

