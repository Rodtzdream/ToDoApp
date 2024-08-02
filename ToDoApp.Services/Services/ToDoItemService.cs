﻿using Microsoft.EntityFrameworkCore;
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

    public async Task UpdateTitleAndDescriptionAsync(int id, ChangeToDoItemDto toDoItemDto)
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

        item.Title = toDoItemDto.Title;
        item.Description = toDoItemDto.Description;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id, UpdateStatusDto newStatus)
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

        var validTransitions = new Dictionary<StatusEnum, HashSet<StatusEnum>>()
    {
        { StatusEnum.ToDo, new HashSet<StatusEnum> { StatusEnum.InProgress } },
        { StatusEnum.InProgress, new HashSet<StatusEnum> { StatusEnum.ToDo, StatusEnum.Done } },
        { StatusEnum.Done, new HashSet<StatusEnum>() }
    };

        if (!validTransitions.ContainsKey((StatusEnum)item.StatusId) || !validTransitions[(StatusEnum)item.StatusId].Contains(newStatus.StatusId))
        {
            throw new ToDoItemStatusNotFoundException();
        }

        item.Status = newStatus.StatusId;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAssigneeAsync(int id, int assigneeId)
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

        item.AssigneeId = assigneeId;
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
