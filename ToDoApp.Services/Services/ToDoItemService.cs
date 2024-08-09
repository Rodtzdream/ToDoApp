using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
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

    public async Task<ToDoItem> GetByIdAsync(int id)
    {
        var item = await _context.ToDoItems
            .Include(x => x.Assignee)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        return item;
    }

    public async Task CreateAsync(CreateToDoItemDto createToDoItemDto)
    {
        var item = new ToDoItem
        {
            Title = createToDoItemDto.Title,
            Description = createToDoItemDto.Description,
            DueDate = createToDoItemDto.DueDate,
            StatusId = StatusEnum.ToDo,
            BoardId = createToDoItemDto.BoardId,
            AssigneeId = _currentUserService.AssigneeId
        };
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTitleAsync(ChangeToDoItemTitleDto changeToDoItemTitleDto)
    {
        var item = await _context.ToDoItems.FindAsync(changeToDoItemTitleDto.Id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(changeToDoItemTitleDto.Id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        item.Title = changeToDoItemTitleDto.Title;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDescriptionAsync(ChangeToDoItemDescriptionDto changeToDoItemDescriptionDto)
    {
        var item = await _context.ToDoItems.FindAsync(changeToDoItemDescriptionDto.Id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(changeToDoItemDescriptionDto.Id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        item.Description = changeToDoItemDescriptionDto.Description;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(UpdateStatusDto newStatus)
    {
        var item = await _context.ToDoItems.FindAsync(newStatus.Id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(newStatus.Id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        var validTransitions = new Dictionary<StatusEnum, HashSet<StatusEnum>>()
    {
        { StatusEnum.ToDo, new HashSet<StatusEnum> { StatusEnum.InProgress } },
        { StatusEnum.InProgress, new HashSet<StatusEnum> { StatusEnum.ToDo, StatusEnum.Done } },
        { StatusEnum.Done, new HashSet<StatusEnum>() }
    };

        if (!validTransitions.ContainsKey(item.StatusId) || !validTransitions[item.StatusId].Contains(newStatus.StatusId))
        {
            throw new ToDoItemStatusNotFoundException();
        }

        item.StatusId = newStatus.StatusId;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAssigneeAsync(ChangeAssigneeDto newAssignee)
    {
        var item = await _context.ToDoItems.FindAsync(newAssignee.Id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(newAssignee.Id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        item.AssigneeId = newAssignee.AssigneeId;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);

        if (item is null)
        {
            throw new ToDoItemNotFoundException(id);
        }

        if (item.AssigneeId != _currentUserService.AssigneeId)
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}
