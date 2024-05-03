using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.InboundPorts;
using TodoList.Application.Services;

namespace TodoList.Application;

public static class ApplicationRegistration
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
    }
}