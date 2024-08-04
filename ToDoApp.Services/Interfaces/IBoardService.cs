using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IBoardService
{
    Task<List<GetBoardDto>> GetAsynk();

    Task CreateAsync(CreateBoardDto createBoardDto);
}
