namespace backend.services.interfaces;

public interface ICurrentUserService
{
  Guid UserId { get; }
  string Role { get; }
  bool IsAuthenticated { get; }
  
  Guid GetRequiredUserId(); //to shorten the validation if there's a userId
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
    var userId = _currentUserService.GetRequiredUserId();    <<<--------

    //rest of the logic....
    
    return "ano ano";
  }
}
*/