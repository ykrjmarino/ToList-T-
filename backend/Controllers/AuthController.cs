using System.Security.Claims;
using backend.DTOs;
using backend.services.interfaces;
using Microsoft.AspNetCore.Authorization;
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

  [HttpPost("login")]
  public async Task<ActionResult> LoginAsync(LoginDto dto)
  {
    var token = await _authService.LoginAsync(dto);

    Response.Cookies.Append(
      "access_token",
      token,
      new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = DateTimeOffset.UtcNow.AddMinutes(10080),
      }
    );

    return Ok(new { message = "Login successful" }); 
  }
[Authorize]
[HttpPost("logout")]
  public async Task<IActionResult> LogoutAsync()
  {
    Response.Cookies.Append("access_token", "", new CookieOptions //server overwrites
    {
    HttpOnly = true,
    Secure = true,
    SameSite = SameSiteMode.None,
    Expires = DateTimeOffset.UtcNow.AddDays(-1) //deletes the token immediately
    });

    await Task.CompletedTask; 
    return Ok(new { message = "Logged out successfully" });
  }

  [Authorize]
  [HttpGet("me")]
  public IActionResult Me()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var role = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
    return Ok(
      new
      {
        userId,
        email,
        role,
      }
    );
  }
}
