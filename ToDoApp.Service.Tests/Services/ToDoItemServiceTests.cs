using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

namespace ToDoApp.Service.Tests.Services;

public class ToDoItemServiceTests
{
    private const string AssigneeId = "1";

    [Fact]
    public async Task GetByIdAsync_WithExistingItem_ReturnsItem()
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var assignee = new User
        {
            Id = AssigneeId,
            UserName = "Test Assignee"
        };

        context.Users.Add(assignee);
        await context.SaveChangesAsync();

        var item = new ToDoItem
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StatusId = StatusEnum.ToDo,
            BoardId = 1,
            AssigneeId = AssigneeId,
        };

        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        var result = await service.GetByIdAsync(item.Id);

        // Assert
        Assert.Equal(item.Id, result.Id);
        Assert.Equal(item.Title, result.Title);
        Assert.Equal(item.Description, result.Description);
        Assert.Equal(item.StatusId, result.StatusId);
        Assert.Equal(item.BoardId, result.BoardId);
        Assert.Equal(item.AssigneeId, result.AssigneeId);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingItem_ThrowsToDoItemNotFoundException()
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        Task act() => service.GetByIdAsync(1);

        // Assert
        await Assert.ThrowsAsync<ToDoItemNotFoundException>(act);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithNonExistingItem_ThrowsToDoItemNotFoundException()
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        Task act() => service.UpdateStatusAsync(1, new ToDoApp.Services.Dtos.UpdateStatusDto() { StatusId = StatusEnum.Done });

        // Assert
        await Assert.ThrowsAsync<ToDoItemNotFoundException>(act);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithDifferentOwner_ThrowsToDoItemHasDifferentOwnerException()
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var item = new ToDoItem
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StatusId = StatusEnum.ToDo,
            BoardId = 1,
            AssigneeId = "2",
        };

        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        Task act() => service.UpdateStatusAsync(item.Id, new ToDoApp.Services.Dtos.UpdateStatusDto() { StatusId = StatusEnum.Done });

        // Assert
        await Assert.ThrowsAsync<ToDoItemHasDifferentOwnerException>(act);
    }

    [Theory]
    [InlineData(StatusEnum.ToDo, StatusEnum.InProgress)]
    [InlineData(StatusEnum.InProgress, StatusEnum.ToDo)]
    public async Task UpdateStatusAsync_TaskStatusIsToggled(StatusEnum currentStatus, StatusEnum expectedStatus)
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var item = new ToDoItem
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StatusId = currentStatus,
            BoardId = 1,
            AssigneeId = AssigneeId,
        };

        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        await service.UpdateStatusAsync(item.Id, new ToDoApp.Services.Dtos.UpdateStatusDto() { StatusId = expectedStatus });

        // Assert
        var updatedItem = await context.ToDoItems.FindAsync(item.Id);
        Assert.Equal(expectedStatus, updatedItem.StatusId);
    }

    [Theory]
    [InlineData(StatusEnum.ToDo, StatusEnum.Done)]
    [InlineData(StatusEnum.Done, StatusEnum.InProgress)]
    [InlineData(StatusEnum.Done, StatusEnum.ToDo)]
    public async Task UpdateStatusAsync_WithInvalidTransition_ThrowsInvalidStatusTransitionException(StatusEnum currentStatus, StatusEnum newStatus)
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var item = new ToDoItem
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StatusId = currentStatus,
            BoardId = 1,
            AssigneeId = AssigneeId,
        };

        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        Task act() => service.UpdateStatusAsync(item.Id, new ToDoApp.Services.Dtos.UpdateStatusDto() { StatusId = newStatus });

        // Assert
        await Assert.ThrowsAsync<ToDoItemStatusNotFoundException>(act);
    }

    [Theory]
    [InlineData(4)]
    public async Task UpdateStatusAsync_WithInvalidStatus_ThrowsInvalidStatusException(int status)
    {
        // Arrange
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(x => x.AssigneeId).Returns(AssigneeId);

        var context = GetDbContext();

        var item = new ToDoItem
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StatusId = StatusEnum.ToDo,
            BoardId = 1,
            AssigneeId = AssigneeId,
        };

        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserServiceMock.Object);

        // Act
        Task act() => service.UpdateStatusAsync(item.Id, new ToDoApp.Services.Dtos.UpdateStatusDto() { StatusId = (StatusEnum)status });

        // Assert
        await Assert.ThrowsAsync<ToDoItemStatusNotFoundException>(act);
    }

    private ToDoContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ToDoContext(options);
    }
}
