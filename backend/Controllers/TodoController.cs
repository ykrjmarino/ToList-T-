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
  public async Task<ActionResult<IEnumerable<TodoResponseDto>>> GetMyTodos([FromQuery] bool? isDeleted = null)
  {
    var res = await _todoService.GetMyTodosAsync(isDeleted); 
        //false-> active
        //true-> deleted
        //null-> all
    return Ok (res);
  }

  [HttpPut("update/{todoId}")]
  public async Task<ActionResult<TodoResponseDto>> UpdateTodo(Guid todoId, UpdateTodoDto dto)
  {
    var res = await _todoService.UpdateTodoAsync(todoId, dto);
    return Ok(res);
  }

  [HttpDelete("{todoId}")] //parameter: isDeleted = true/false
  public async Task<IActionResult> SetTodoDeleted( //IActionResult => "Just returning an HTTP result, not data"
    Guid todoId,
    bool? isDeleted = null)
  {
    var message = await _todoService.SetTodoDeletedAsync(todoId, isDeleted);
    return Ok(new { message });
  }
}
