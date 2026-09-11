using backend.DTOs.todo;

namespace backend.services.interfaces;

public interface ITodoService
{
  Task<TodoResponseDto> CreateTodoAsync(CreateTodoDto dto);
  Task<IEnumerable<TodoResponseDto>> GetMyTodosAsync(bool? isDeleted = null);
  Task<TodoResponseDto> UpdateTodoAsync(Guid todoId, UpdateTodoDto dto);
  Task<string> SetTodoDeletedAsync(Guid todoId, bool? isDeleted = null);
}
