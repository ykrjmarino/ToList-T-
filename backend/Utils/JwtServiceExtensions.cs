using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace backend.utils;

public static class JwtServiceExtensions
{
  public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    var jwtKey = configuration["Jwt:Key"];
    var jwtIssuer = configuration["Jwt:Issuer"];
    var jwtAudience = configuration["Jwt:Audience"];

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
      };

      options.Events = new JwtBearerEvents
      {
        OnMessageReceived = context =>
        {
          context.Token = context.Request.Cookies["access_token"];
          return Task.CompletedTask;
        },
        OnForbidden = async context =>
        {
          context.Response.StatusCode = StatusCodes.Status403Forbidden;
          context.Response.ContentType = "application/json";
          var payload = JsonSerializer.Serialize(new { message = "Forbidden: Access denied. You do not have the required Administrator permissions." });
          await context.Response.WriteAsync(payload);
        },

        // 2. Triggers when the cookie/token is completely missing or invalid
        OnChallenge = async context =>
        {
          context.HandleResponse(); // Stops default .NET MVC redirect behavior
          context.Response.Headers.Append("WWW-Authenticate", "Bearer");
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          context.Response.ContentType = "application/json";
          var payload = JsonSerializer.Serialize(new { message = "Unauthorized: Access token is missing, expired, or invalid." });
          await context.Response.WriteAsync(payload);
        }
      };
    });
    return services;
  }

}

//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
//dotnet add package System.IdentityModel.Tokens.Jwt