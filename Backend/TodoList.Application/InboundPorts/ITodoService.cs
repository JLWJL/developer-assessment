using TodoList.Domain;

namespace TodoList.Application.InboundPorts;

public interface ITodoService
{
    Task<TodoItem?> GetItemById(Guid id);
    Task<IEnumerable<TodoItem>> GetAllItemsAsync();
    Task<TodoItem> UpdateItemAsync(Guid id, TodoItem item);
    Task<TodoItem> AddItemAsync(TodoItem item);
}