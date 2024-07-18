namespace ToDoApp.Data.Models;

public enum StatusEnum
{
    ToDo = 1,
    InProgress = 2,
    Done = 3
}

public class Status
{
    public int Id { get; set; }

    public string Name { get; set; }
}