using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Services;

namespace ToDoApp.Service.Tests.Services;

public class BoardServiceTests
{
    private const int AssigneeId = 1;

    [Fact]
    public async Task GetByIdAsync_WithExistingBoard_ReturnsBoard()
    {
        // Arrange
        var context = GetDbContext();

        var board = new Board
        {
            Id = 1,
            Name = "Test Board"
        };

        context.Boards.Add(board);
        await context.SaveChangesAsync();

        var service = new BoardService(context);

        // Act
        var result = await service.GetByIdAsynk(board.Id);

        // Assert
        Assert.Equal(board.Id, result.Id);
        Assert.Equal(board.Name, result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingBoard_ThrowsBoardNotFoundException()
    {
        // Arrange
        var context = GetDbContext();

        var service = new BoardService(context);

        // Act
        async Task Act() => await service.GetByIdAsynk(1);

        // Assert
        await Assert.ThrowsAsync<BoardNotFoundException>(Act);
    }

    private ToDoContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ToDoContext(options);
    }
}
