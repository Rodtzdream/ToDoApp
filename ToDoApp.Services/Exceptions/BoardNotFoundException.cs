using System.Net;

namespace ToDoApp.Services.Exceptions;

public class BoardNotFoundException : ApplicationBaseException
{
    public BoardNotFoundException(int id) : base($"Board with id {id} was not found", HttpStatusCode.NotFound)
    {
    }
}
