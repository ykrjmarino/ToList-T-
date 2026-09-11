using backend.DTOs;

namespace backend.services.interfaces;

public interface IAuthService
{
  Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto);
  Task<string> LoginAsync(LoginDto dto);
}
