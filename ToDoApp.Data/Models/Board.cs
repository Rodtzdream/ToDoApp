namespace ToDoApp.Data.Models;

public class Board
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<ToDoItem> ToDoItems { get; set; }
}