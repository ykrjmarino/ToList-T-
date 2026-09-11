using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.todo;

public class CreateTodoDto //Title, Description
{
  [Required]
  [MaxLength(100, ErrorMessage = "Title should be within 100 characters.")]
  public required string Title {get; set;}

  [MaxLength(600, ErrorMessage = "Description should be within 600 characters.")]
  public string Description { get; set; } = string.Empty;
}
