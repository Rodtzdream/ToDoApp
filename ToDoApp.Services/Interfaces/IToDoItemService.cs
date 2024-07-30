using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IToDoItemService
{
    Task<List<ToDoItem>> GetAsynk();

    Task CreateAsync(CreateToDoItemDto createToDoItemDto);

    Task UpdateStatusAsync(int id);
}
