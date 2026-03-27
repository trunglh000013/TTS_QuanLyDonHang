using System.Collections.Generic;

namespace ProductTest.Application.DTOs.Response.User;

public sealed record LoginUserResponse
{
    public bool Success { get; set; }
}
