using backend.enums;

namespace backend.DTOs.todo;

public class TodoResponseDto
{
  public Guid TodoId { get; set; }
  public Guid UserId { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public TodoStatus Status { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastUpdatedAt { get; set; }

  public bool IsDeleted { get; set; }
  public DateTime? DeletedAt { get; set; }
}
