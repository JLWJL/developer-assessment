using TodoList.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TodoList.Application.OutboundPorts;
using TodoList.Domain;

namespace TodoList.Tests.InboundServices;

public class TodoServiceTests
{ 
    private readonly Mock<ITodoRepository> _todoRepoMock;
    private readonly TodoService _todoService;

    public TodoServiceTests()
    {
        _todoRepoMock = new Mock<ITodoRepository>();
        _todoService = new TodoService(_todoRepoMock.Object);
    }
    
    [Fact]
    public async Task GetItemById_ValidId_ReturnsItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var expectedItem = new TodoItem { Id = itemId, Description = "Test item", IsCompleted = false };
        _todoRepoMock.Setup(repo => repo.GetAsync(itemId)).ReturnsAsync(expectedItem);

        // Act
        var result = await _todoService.GetItemById(itemId);

        // Assert
        Assert.Equal(expectedItem, result);
    }
    
    [Fact]
    public async Task GetItemById_InValidId_ReturnsNull()
    {
        // Arrange
        var itemId = Guid.NewGuid();

        // Act
        var result = await _todoService.GetItemById(itemId);

        // Assert
        Assert.Equal(null, result);
    }

    [Fact]
    public async Task GetAllItemsAsync_ReturnsAllItems()
    {
        // Arrange
        var expectedItems = new List<TodoItem>
        {
            new () { Id = Guid.NewGuid(), Description = "Item 1", IsCompleted = false },
            new () { Id = Guid.NewGuid(), Description = "Item 2", IsCompleted = true }
        };
        _todoRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedItems);

        // Act
        var result = await _todoService.GetAllItemsAsync();

        // Assert
        Assert.Equal(expectedItems, result);
    }

    [Fact]
    public async Task UpdateItemAsync_ValidId_ReturnsUpdatedItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var existingItem = new TodoItem { Id = itemId, Description = "Existing item", IsCompleted = false };
        var updatedItem = new TodoItem { Id = itemId, Description = "Updated item", IsCompleted = true };
        
        _todoRepoMock.Setup(repo => repo.GetAsync(itemId)).ReturnsAsync(existingItem);
        _todoRepoMock.Setup(repo => repo.UpdateAsync(itemId, existingItem)).ReturnsAsync(updatedItem);

        // Act
        var result = await _todoService.UpdateItemAsync(itemId, updatedItem);

        // Assert
        Assert.Equal(updatedItem, result);
    }
    
    [Fact]
    public async Task UpdateItemAsync_InValidId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var existingItem = new TodoItem { Id = itemId, Description = "Existing item", IsCompleted = false };
        var updatedItem = new TodoItem { Id = itemId, Description = "Updated item", IsCompleted = true };
        
        _todoRepoMock.Setup(repo => repo.GetAsync(Guid.NewGuid())).ReturnsAsync(null as TodoItem);
        // _todoRepoMock.Setup(repo => repo.UpdateAsync(itemId, existingItem)).ReturnsAsync(updatedItem);

        // Act
        var result = async ()=> await _todoService.UpdateItemAsync(itemId, updatedItem);

        // Assert
        Assert.ThrowsAsync<KeyNotFoundException>(result);
    }

    [Fact]
    public async Task AddItemAsync_ValidItem_ReturnsAddedItem()
    {
        // Arrange
        var newItem = new TodoItem { Id = Guid.NewGuid(), Description = "New item", IsCompleted = false };
        _todoRepoMock.Setup(repo => repo.AddAsync(newItem)).ReturnsAsync(newItem);
        
        // Act
        var result = await _todoService.AddItemAsync(newItem);

        // Assert
        Assert.Equal(newItem, result);
    }
}