using backend.DTOs;
using backend.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
  private readonly IAuthService _authService = authService;

  [HttpPost("register")]
  public async Task<ActionResult<RegisterUserResponseDto>> RegisterUser([FromBody] RegisterUserDto dto)
  {
    var res = await _authService.RegisterUserAsync(dto);
    return Ok(res);
  }
}
