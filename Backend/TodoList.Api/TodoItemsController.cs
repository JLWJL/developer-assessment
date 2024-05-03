using Microsoft.AspNetCore.Mvc;
using TodoList.Application.InboundPorts;
using TodoList.Domain;

namespace TodoList.Api;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoItemsController> _logger;

    public TodoItemsController( ITodoService todoService, ILogger<TodoItemsController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<IActionResult> GetTodoItems()
    {
        var results = await _todoService.GetAllItemsAsync();
        return Ok(results);
    }

    // GET: api/TodoItems/...
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoItem(Guid id)
    {
        var result = await _todoService.GetItemById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // PUT: api/TodoItems/... 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }
        
        return Ok(await _todoService.UpdateItemAsync(id, todoItem));
    } 

    // POST: api/TodoItems 
    [HttpPost]
    public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
    {
        //Assume Description is required but can be duplicated
        if (string.IsNullOrEmpty(todoItem?.Description))
        {
            return BadRequest("Description is required");
        }
    
        var result = await _todoService.AddItemAsync(todoItem);
        return CreatedAtAction(nameof(GetTodoItem), new { id = result.Id }, result);
    } 
}