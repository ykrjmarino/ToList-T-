using backend.DTOs.todo;

namespace backend.services.interfaces;

public interface ITodoService
{
  Task<TodoResponseDto> CreateTodoAsync(CreateTodoDto dto);
  Task<IEnumerable<TodoResponseDto>> GetMyTodosAsync();
}
