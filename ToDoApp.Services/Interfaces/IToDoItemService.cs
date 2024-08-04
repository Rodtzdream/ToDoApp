using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IToDoItemService
{
    Task<List<ToDoItem>> GetAsynk();

    Task CreateAsync(CreateToDoItemDto createToDoItemDto);

    Task UpdateTitleAsync(int id, ChangeToDoItemTitleDto changeToDoItemTitleDto);

    Task UpdateDescriptionAsync(int id, ChangeToDoItemDescriptionDto changeToDoItemDescriptionDto);

    Task UpdateStatusAsync(int id, UpdateStatusDto newStatus);

    Task UpdateAssigneeAsync(int id, int assigneeId);

    Task DeleteAsync(int id);
}
