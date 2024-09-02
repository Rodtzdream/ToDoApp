using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class BoardService : IBoardService
{
    private readonly ToDoContext _context;

    public BoardService(ToDoContext toDoContext)
    {
        _context = toDoContext;
    }

    public async Task<List<GetBoardDto>> GetAsynk()
    {
        return await _context.Boards.Select(board => new GetBoardDto
        {
            Id = board.Id,
            Name = board.Name,
            CreatedAt = board.CreatedAt,
            ToDoItems = board.ToDoItems.Select(toDoItem => new GetToDoItemDto
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                CreatedAt = toDoItem.CreatedAt,
                DueDate = toDoItem.DueDate,
                StatusId = toDoItem.StatusId,
                BoardId = toDoItem.BoardId,
                AssigneeId = toDoItem.AssigneeId
            }).ToList()
        }).ToListAsync();
    }

    public async Task<GetBoardDto> GetByIdAsynk(int id)
    {
        var board = await _context.Boards
                .Include(board => board.ToDoItems)
                .FirstOrDefaultAsync(board => board.Id == id);

        if (board == null)
        {
            throw new BoardNotFoundException(id);
        }

        return new GetBoardDto
        {
            Id = board.Id,
            Name = board.Name,
            CreatedAt = board.CreatedAt,
            ToDoItems = board.ToDoItems.Select(toDoItem => new GetToDoItemDto
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                CreatedAt = toDoItem.CreatedAt,
                DueDate = toDoItem.DueDate,
                StatusId = toDoItem.StatusId,
                BoardId = toDoItem.BoardId,
                AssigneeId = toDoItem.AssigneeId
            }).ToList()
        };
    }

    public Task CreateAsync(CreateBoardDto createBoardDto)
    {
        var board = new Board
        {
            Name = createBoardDto.Name
        };

        _context.Boards.Add(board);
        return _context.SaveChangesAsync();
    }
}
