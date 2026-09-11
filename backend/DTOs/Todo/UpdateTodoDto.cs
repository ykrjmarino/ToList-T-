using System.ComponentModel.DataAnnotations;
using backend.enums;

namespace backend.DTOs.todo;

public class UpdateTodoDto
{
  [MaxLength(100, ErrorMessage = "Title should be within 100 characters.")]
  public string ? Title {get; set;}

  [MaxLength(600, ErrorMessage = "Description should be within 600 characters.")]
  public string ? Description { get; set; }

  public TodoStatus ? Status { get; set; }
}
