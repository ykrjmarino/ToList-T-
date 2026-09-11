using backend.data;
using backend.DTOs.todo;
using backend.enums;
using backend.exceptions;
using backend.models;
using backend.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class TodoService(
  AppDbContext dbContext,
  ICurrentUserService currentUserService
) : ITodoService
{
  private readonly AppDbContext _dbContext = dbContext;
  private readonly ICurrentUserService _currentUserService = currentUserService;

  // =============================== CREATE =============================== //
  public async Task<TodoResponseDto> CreateTodoAsync(CreateTodoDto dto)
  {
    //from ICurrentUserService --httpContextAccessor
    var userId = _currentUserService.GetRequiredUserId();

    //store user input (DTO)
    Todo todo = new()
    {
      UserId = userId,
      Status = TodoStatus.Todo,
      Title = dto.Title,
      Description = dto.Description.Trim()
    };

    //store in db context
    _dbContext.Todos.Add(todo);

    //save to db
    await _dbContext.SaveChangesAsync();

    return new TodoResponseDto
    {
      TodoId = todo.TodoId,
      UserId = todo.UserId,
      Title = todo.Title,
      Description = todo.Description,
      Status = todo.Status,
      CreatedAt = todo.CreatedAt,
      LastUpdatedAt = todo.LastUpdatedAt,
      IsDeleted = todo.IsDeleted,
      DeletedAt = todo.DeletedAt
    }; 
  }

  // =============================== GET ALL =============================== //
  public async Task<IEnumerable<TodoResponseDto>> GetMyTodosAsync(bool? isDeleted = null)
  {
    var userId = _currentUserService.GetRequiredUserId();

    return await _dbContext.Todos
      .Where(t => t.UserId == userId && (!isDeleted.HasValue || t.IsDeleted == isDeleted.Value))
      .Select(t => new TodoResponseDto
      {
        TodoId = t.TodoId,
        UserId = t.UserId,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        CreatedAt = t.CreatedAt,
        LastUpdatedAt = t.LastUpdatedAt,
        IsDeleted = t.IsDeleted,
        DeletedAt = t.DeletedAt
      }).ToListAsync();
  }
  
  // =============================== UPDATE =============================== //
  public async Task<TodoResponseDto> UpdateTodoAsync(Guid todoId, UpdateTodoDto dto)
  {
    var userId = _currentUserService.GetRequiredUserId();

    var todo = await _dbContext.Todos
      .FirstOrDefaultAsync(t => 
        t.TodoId == todoId &&
        t.UserId == userId)
     ?? throw new ResourceNotFoundException("Todo not found");

    todo.Title = dto.Title?.Trim() ?? todo.Title;
    todo.Description = dto.Description?.Trim() ?? todo.Description;
    todo.LastUpdatedAt = DateTime.UtcNow;
    if (dto.Status.HasValue) 
      todo.Status = dto.Status.Value;

    await _dbContext.SaveChangesAsync();

    return new TodoResponseDto
    {
      TodoId = todo.TodoId,
      UserId = todo.UserId,
      Title = todo.Title,
      Description = todo.Description,
      Status = todo.Status,
      CreatedAt = todo.CreatedAt,
      LastUpdatedAt = todo.LastUpdatedAt,
      IsDeleted = todo.IsDeleted,
      DeletedAt = todo.DeletedAt
    };
  }
  
  // =============================== DELETE & RESTORE =============================== //
  public async Task<string> SetTodoDeletedAsync(Guid todoId, bool? isDeleted = null)
  {
    var userId = _currentUserService.GetRequiredUserId();
    
    var todo = await _dbContext.Todos
      .FirstOrDefaultAsync(t => t.TodoId == todoId && t.UserId == userId) 
     ?? throw new ResourceNotFoundException("Todo not found.");

    bool targetDeletedState = isDeleted ?? true; //if no input (null) -> true

    if (todo.IsDeleted == targetDeletedState) //todo.IsDeleted(from db) == isDeleted (input)
     return targetDeletedState
      ? "Todo is already deleted."
      : "Todo is aleady restored.";

    todo.IsDeleted = targetDeletedState;
    todo.DeletedAt = targetDeletedState ? DateTime.UtcNow : null;
    todo.LastUpdatedAt = DateTime.UtcNow;

    await _dbContext.SaveChangesAsync();

    return targetDeletedState
      ? "Todo deleted successfully."
      : "Todo restored successfully.";
  }
}
