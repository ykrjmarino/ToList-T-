using backend.data;
using backend.DTOs.todo;
using backend.enums;
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
    var userId = _currentUserService.UserId;
    if (userId == Guid.Empty) throw new UnauthorizedAccessException("No authenticated user found.");

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
      LastUpdatedAt = todo.LastUpdatedAt
    }; 
  }

  // =============================== GET ALL =============================== //
  public async Task<IEnumerable<TodoResponseDto>> GetMyTodosAsync()
  {
    var userId = _currentUserService.UserId;
    if (userId == Guid.Empty) throw new UnauthorizedAccessException("No authenticated user found.");

    return await _dbContext.Todos
      .Where(t => t.UserId == userId)
      .Select(t => new TodoResponseDto
      {
        TodoId = t.TodoId,
        UserId = t.UserId,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        CreatedAt = t.CreatedAt,
        LastUpdatedAt = t.LastUpdatedAt
      }).ToListAsync();
  }

}
// Get user's todos
// Get one todo
// Create
// Update
// Delete