using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain;
using TodoList.Infrastructure;
using TodoList.Infrastructure.Repositories;
using Xunit;

namespace TodoList.Tests.Infrastructure;

public class TodoRepositoryTest
{
    private readonly TodoRepository _sut;
    private readonly TodoContext mockContext;
    private List<TodoItem> _todoItems = new (){
        new() { Description = "task 1", Id = Guid.NewGuid(), IsCompleted = true },
        new() { Description = "task 2", Id = Guid.NewGuid(), IsCompleted = false },
        new() { Description = "task 3", Id = Guid.NewGuid(), IsCompleted = true },
    };
    
    public TodoRepositoryTest()
    {
        var contextOptions = new DbContextOptionsBuilder<TodoContext>(
        ).UseInMemoryDatabase("TodoItemsDatabase").Options;

        mockContext = new TodoContext(contextOptions);

        mockContext.Database.EnsureDeleted();
        mockContext.Database.EnsureCreated();
        
        mockContext.AddRange(_todoItems);
        mockContext.SaveChanges();
        
        _sut = new(mockContext);    
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetAsync_ReturnItemById(int itemIndex)
    {
        //Arrange
        var item = _todoItems[itemIndex];
        
        //Act
        var res = await _sut.GetAsync(item.Id);
        
        //Assert
        Assert.Equal(item, res);
    }
    
    [Fact]
    public async Task GetAsync_NoIdMatched_ReturnNull()
    {
        //Arrange
        var id = Guid.NewGuid();
        
        //Act
        var res = await _sut.GetAsync(id);
        
        //Assert
        Assert.Equivalent(null, res);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        //Act
        var res = await _sut.GetAllAsync();
        
        //Assert
        Assert.Equal(_todoItems, res);
    }

    [Fact]
    public async Task UpdateAsync_UpdateItemWithChanges()
    {
        //Arrange
        var item = _todoItems[0];
        item.Description = "Description updated";
        item.IsCompleted = !item.IsCompleted;
        
        //Act
        var result = await _sut.UpdateAsync(item.Id, item);
        
        //Assert
        Assert.Equal(result, item);
    }
    
    [Fact]
    public async Task AddAsync_AddNewItem()
    {
        //Arrange
        var newItem = new TodoItem{ Description = "New task", Id = Guid.NewGuid(), IsCompleted = false };
        
        //Act    
        var result = await _sut.AddAsync(newItem);
        
        //Assert
        Assert.Equal(result, newItem);
    }
}