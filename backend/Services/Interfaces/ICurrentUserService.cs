namespace backend.services.interfaces;

public interface ICurrentUserService
{
  Guid UserId { get; }
  string Role { get; }
  bool IsAuthenticated { get; }
}

/*
Use it like this:

public class TryService(
  AppDbContext dbContext,
  ICurrentUserService currentUserService   <<<--------
) : ITryService
{
  private readonly AppDbContext _dbContext = dbContext;

  public async Task<string> TryAsync(TryDTO dto)
  {
    // Get the ID cleanly without digging through HttpContext layers
    var userId = currentUserService.UserId;
    if (userId == Guid.Empty) throw new UnauthorizedAccessException("No authenticated user found.");

    //rest of the logic....
    
    return "ano ano";
  }
}
*/