using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IToDoItemService
{
    Task<List<ToDoItem>> GetAsynk();

    Task CreateAsync(CreateToDoItemDto createToDoItemDto);

    Task UpdateTitleAndDescriptionAsync(StatusEnum id, ChangeToDoItemDto toDoItemDto);

    Task UpdateStatusAsync(StatusEnum id, UpdateStatusDto newStatus);

    Task UpdateAssigneeAsync(StatusEnum id, int assigneeId);

    Task DeleteAsync(StatusEnum id);
}
