using System.Net;

namespace ToDoApp.Services.Exceptions;

public class ToDoItemStatusNotFoundException : ApplicationBaseException
{
    public ToDoItemStatusNotFoundException() : base("ToDo item status doesn't exist", HttpStatusCode.BadRequest)
    {
    }
}
