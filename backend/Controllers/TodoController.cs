using backend.DTOs.todo;
using backend.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers;

[Authorize]
[Route("api/todo")]
[ApiController]
public class TodoController (ITodoService todoService) : ControllerBase
{
  private readonly ITodoService _todoService = todoService;

  [HttpPost("create")]
  public async Task<ActionResult<TodoResponseDto>> CreateTodo([FromBody] CreateTodoDto dto)
  {
    var res = await _todoService.CreateTodoAsync(dto);
    return Ok (res);
  }

  [HttpGet]
  public async Task<ActionResult<TodoResponseDto>> GetMyTodos()
  {
    var res = await _todoService.GetMyTodosAsync();
    return Ok (res);
  }
}
