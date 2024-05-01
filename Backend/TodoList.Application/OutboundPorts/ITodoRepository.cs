using TodoList.Domain;

namespace TodoList.Application.OutboundPorts;

public interface ITodoRepository
{
    Task<TodoItem?> GetAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem> UpdateAsync();
    Task<TodoItem> AddAsync();
}