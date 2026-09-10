using backend.services.interfaces;
using BCrypt.Net;

namespace backend.services;

public class PasswordHasher : IPasswordHasher
{
  public string HashPassword(string plainPassword)
  {
    return BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: 12);
  }

  public bool VerifyPassword(string plainPassword, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
  }
}

//dotnet add package BCrypt.Net-Next