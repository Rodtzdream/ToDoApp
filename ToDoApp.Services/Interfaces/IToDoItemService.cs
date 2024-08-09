using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IToDoItemService
{
    Task<List<ToDoItem>> GetAsynk();

    Task<ToDoItem> GetByIdAsync(int id);

    Task CreateAsync(CreateToDoItemDto createToDoItemDto);

    Task UpdateTitleAsync(ChangeToDoItemTitleDto changeToDoItemTitleDto);

    Task UpdateDescriptionAsync(ChangeToDoItemDescriptionDto changeToDoItemDescriptionDto);

    Task UpdateStatusAsync(UpdateStatusDto newStatus);

    Task UpdateAssigneeAsync(ChangeAssigneeDto newAssignee);

    Task DeleteAsync(int id);
}
