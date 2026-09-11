using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.models;
using Microsoft.IdentityModel.Tokens;

namespace backend.utils;

public class JwtTokenGenerator(IConfiguration configuration)
{
  private readonly IConfiguration _configuration = configuration;

  public string GenerateToken(User user)
  {
    var jwtKey = _configuration["Jwt:Key"];
    var jwtIssuer = _configuration["Jwt:Issuer"];
    var jwtAudience = _configuration["Jwt:Audience"];
    var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]!);

    var claims = new List<Claim>
    {
      new (ClaimTypes.NameIdentifier, user.UserId.ToString()),
      new (ClaimTypes.Email, user.Email),
      new (ClaimTypes.Role, user.Role.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: jwtIssuer,
      audience: jwtAudience,
      claims: claims,
      expires: DateTime.UtcNow.AddMinutes(expireMinutes),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
