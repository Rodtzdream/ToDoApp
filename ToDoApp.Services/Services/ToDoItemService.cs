using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly ToDoContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ToDoItemService(ToDoContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<ToDoItem>> GetAsynk()
    {
        return await _context.ToDoItems
            .Where(x => x.AssigneeId == _currentUserService.AssigneeId)
            .ToListAsync();
    }

    public async Task CreateAsync(CreateToDoItemDto createToDoItemDto)
    {
        var item = new ToDoItem
        {
            Title = createToDoItemDto.Title,
            Description = createToDoItemDto.Description,
            DueDate = createToDoItemDto.DueDate,
            Status = StatusEnum.ToDo,
            BoardId = createToDoItemDto.BoardId,
            AssigneeId = _currentUserService.AssigneeId
        };
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);

        if (item is null) { }

        if (item.AssigneeId != _currentUserService.AssigneeId) { }

        if (item.StatusId == (int)StatusEnum.ToDo)
        {
            item.Status = StatusEnum.InProgress;
        }
        else if (item.StatusId == (int)StatusEnum.InProgress)
        {
            item.Status = StatusEnum.Done;
        }
        await _context.SaveChangesAsync();
    }
}
