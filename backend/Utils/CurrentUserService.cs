using System.Security.Claims;
using  backend.services.interfaces;

namespace backend.utils;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
public Guid UserId 
  {
    get
    {
      var claimValue = httpContextAccessor.HttpContext?.User?
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

      return Guid.TryParse(claimValue, out var guid) ? guid : Guid.Empty;
    }
  }

  public string Role => httpContextAccessor.HttpContext?.User?
    .FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

  public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
