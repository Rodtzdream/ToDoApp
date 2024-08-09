using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class UserService : IUserService
{
    private readonly ToDoContext _context;

    public UserService(ToDoContext context)
    {
        _context = context;
    }

    public async Task<List<GetUserDto>> GetUsersAsync()
    {
        return await _context.Users.Select(x => new GetUserDto
        {
            Id = x.Id,
            UserName = x.UserName!,
        }).ToListAsync();
    }

    public async Task<GetUserDto> GetUserByIdAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return new GetUserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
        };
    }
}
