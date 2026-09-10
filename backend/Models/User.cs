namespace backend.models;

public class User
{
  //[Key] //primary key of this table --can remove because we have ".HasKey" in UserConfiguration.cs
  public Guid UserId {get; set;}

  public required string Username {get; set;}                         //column
  public required string Email {get; set;}                            //column
  public required string PasswordHash {get; set;}                     //column

  public string Bio { get; set; } = string.Empty;                     //column
  public bool IsActive { get; set; } = true;                          //column

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;          //column
  public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;      //column

  //navigation: User can navigate to its Todos
  public ICollection<Todo> Todos { get; set; } = [];                  //navigation
  
}
