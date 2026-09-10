namespace backend.services.interfaces;

public interface IPasswordHasher
{
  string HashPassword(string plainPassword);
  bool VerifyPassword(string plainPassword, string hashedPassword);
}