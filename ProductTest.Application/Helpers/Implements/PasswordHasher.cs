using BCrypt.Net;
using ProductTest.Application.Abstractions.Helpers;

namespace ProductTest.Application.Helpers.Implements;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        Console.WriteLine("Hashed password: " + hashedPassword);
        Console.WriteLine("Provided password: " + providedPassword);
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
