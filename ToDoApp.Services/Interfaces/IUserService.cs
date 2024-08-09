using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IUserService
{
    Task<List<GetUserDto>> GetUsersAsync();
    Task<GetUserDto> GetUserByIdAsync(string id);
}
