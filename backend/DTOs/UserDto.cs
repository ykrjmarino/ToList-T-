namespace backend.DTOs;

public class RegisterUserResponseDto
{
  public Guid UserId { get; set; }
  public string Email { get; set; } = string.Empty;
  public string Username { get; set; } = string.Empty;
}

/*
public class UserProfileResponseDto //not used yet
{
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Bio { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
}
*/