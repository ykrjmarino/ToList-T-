using backend.data;
using backend.DTOs;
using backend.exceptions;
using backend.models;
using backend.services.interfaces;
using Microsoft.EntityFrameworkCore;


namespace backend.services;

public class AuthService (
  AppDbContext dbContext,
  IPasswordHasher passwordHasher
) : IAuthService
{
  private readonly AppDbContext _dbContext = dbContext;
  private readonly IPasswordHasher _passwordHasher = passwordHasher;

  // =============================== REGISTER =============================== //
  public async Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto)
  {
    var normalizedEmail = dto.Email.Trim().ToLower();
    var normalizedUsername = dto.Username.Trim().ToLower();

    //check email if existing --boolean
    var emailExists = await _dbContext.Users
      .AnyAsync(u => u.Email == normalizedEmail);

    if (emailExists)
      throw new EmailAlreadyExistsException("A user with this email address already exists.");

    var usernameExists = await _dbContext.Users
      .AnyAsync(u => u.Username == normalizedUsername);

    if (usernameExists)
      throw new UsernameAlreadyExistsException("A user with this username already exists.");

    var newUser = new User //db
    {
      Email = normalizedEmail,
      PasswordHash = _passwordHasher.HashPassword(dto.Password),
      Username = dto.Username.Trim().ToLower()
    };

    _dbContext.Users.Add(newUser); //add user
    await _dbContext.SaveChangesAsync();

    return new RegisterUserResponseDto
    {
      UserId = newUser.UserId,
      Email = newUser.Email,
      Username = newUser.Username
    };
  }
}
