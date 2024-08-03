using System.Net;
using ToDoApp.Data.Models;

namespace ToDoApp.Services.Exceptions;

public class ToDoItemNotFoundException : ApplicationBaseException
{
    public ToDoItemNotFoundException(int id) : base($"ToDo item with id {id} was not found", HttpStatusCode.NotFound)
    {
    }
}
