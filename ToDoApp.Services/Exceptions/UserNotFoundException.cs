using System.Net;

namespace ToDoApp.Services.Exceptions;

public class UserNotFoundException : ApplicationBaseException
{
    public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound)
    {
    }
}
