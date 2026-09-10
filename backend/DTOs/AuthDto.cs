using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class RegisterUserDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email address")]
  public required string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password is required")]
  [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
  [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
  public required string Password { get; set; } = string.Empty;

  [Required(ErrorMessage = "Confirm password is required")]
  [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
  public required string ConfirmPassword { get; set; } = string.Empty;

  [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
  [MinLength(2, ErrorMessage = "Username must be at least 2 characters long")]
  public string Username { get; set; } = string.Empty;
}
