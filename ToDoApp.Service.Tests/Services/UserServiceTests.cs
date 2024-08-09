using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Services;

namespace ToDoApp.Service.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public async Task GetUsersAsync_WithExistingUsers_ReturnsUsers()
    {
        // Arrange
        var context = GetDbContext();

        var user1 = new User
        {
            Id = "1",
            UserName = "Test User 1"
        };

        var user2 = new User
        {
            Id = "2",
            UserName = "Test User 2"
        };

        context.Users.Add(user1);
        context.Users.Add(user2);
        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.GetUsersAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(user1.Id, result[0].Id);
        Assert.Equal(user1.UserName, result[0].UserName);
        Assert.Equal(user2.Id, result[1].Id);
        Assert.Equal(user2.UserName, result[1].UserName);
    }

    [Fact]
    public async Task GetUserByIdAsync_WithExistingUser_ReturnsUser()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Id = "1",
            UserName = "Test User"
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.GetUserByIdAsync(user.Id);

        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.UserName, result.UserName);
    }

    private ToDoContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ToDoContext(options);
    }
}
