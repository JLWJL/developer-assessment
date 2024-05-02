using Microsoft.EntityFrameworkCore;
using TodoList.Application.OutboundPorts;
using TodoList.Domain;

namespace TodoList.Infrastructure.Repositories;

public class TodoRepository: ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TodoItem?> GetAsync(Guid id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> UpdateAsync(Guid id, TodoItem updatedItem)
    {
        var result = _context.TodoItems.Update(updatedItem);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<TodoItem> AddAsync(TodoItem addedItem)
    {
        var result = _context.TodoItems.Add(addedItem);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}