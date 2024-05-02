using TodoList.Application.InboundPorts;
using TodoList.Application.OutboundPorts;
using TodoList.Domain;

namespace TodoList.Application.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepo;

    public TodoService(ITodoRepository todoRepo)
    {
        _todoRepo = todoRepo ?? throw new ArgumentNullException(nameof(todoRepo));
    }

    public async Task<TodoItem?> GetItemById(Guid id)
    {
        return await _todoRepo.GetAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllItemsAsync()
    {
        return await _todoRepo.GetAllAsync();
    }

    public async Task<TodoItem> UpdateItemAsync(Guid id, TodoItem updatedItem)
    {
        var item = await _todoRepo.GetAsync(id) ?? throw new KeyNotFoundException($"Item with id {id} is not found");
        item.Description = updatedItem.Description;
        item.IsCompleted = updatedItem.IsCompleted;
        
        return await _todoRepo.UpdateAsync(id, item);
    }

    public async Task<TodoItem> AddItemAsync(TodoItem newItem)
    {
        return await _todoRepo.AddAsync(newItem);
    }
}