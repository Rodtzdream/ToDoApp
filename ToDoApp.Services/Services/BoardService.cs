using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class BoardService : IBoardService
{
    private readonly ToDoContext _context;

    public BoardService(ToDoContext toDoContext)
    {
        _context = toDoContext;
    }

    public async Task<List<Board>> GetAsynk()
    {
        return await _context.Boards.ToListAsync();
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
