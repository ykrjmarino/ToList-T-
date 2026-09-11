using backend.enums;

namespace backend.models;

public class Todo
{
  public Guid TodoId { get; set; }                                    //column
  public Guid UserId { get; set; } //ForeignKey                       //column

  public required string Title { get; set; }                          //column
  public string Description { get; set; } = string.Empty;             //column
  public TodoStatus Status { get; set; }                              //column --enum

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;          //column
  public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;      //column
  
  //navigation: a Todo can access the User who owns it
  public User ? User { get; set; }                                      //navigation
    //we can remove this, this is just a door for Todo to know who owns it
}
