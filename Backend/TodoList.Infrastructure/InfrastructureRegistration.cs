using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.OutboundPorts;
using TodoList.Infrastructure.Repositories;

namespace TodoList.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddDataPersistence(this IServiceCollection services)
    {
        services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoItemsDB"));
        services.AddScoped<ITodoRepository, TodoRepository>();
    }
}