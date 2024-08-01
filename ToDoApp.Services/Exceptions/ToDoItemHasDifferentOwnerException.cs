using System.Net;

namespace ToDoApp.Services.Exceptions;

public class ToDoItemHasDifferentOwnerException : ApplicationBaseException
{
    public ToDoItemHasDifferentOwnerException() : base("ToDo item has different owner", HttpStatusCode.Forbidden)
    {
    }
}
