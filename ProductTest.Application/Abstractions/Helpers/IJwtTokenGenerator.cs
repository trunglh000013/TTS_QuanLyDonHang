using System.Security.Claims;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.Helpers;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(string userId, string email, IEnumerable<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromToken(string token);
}
