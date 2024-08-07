namespace ToDoApp.Data.Models;

public class ToDoItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int BoardId { get; set; }

    public Board Board { get; set; }

    public DateTime CreatedAt { get; set; }

    public StatusEnum StatusId { get; set; }

    public DateTime DueDate { get; set; }

    public string AssigneeId { get; set; }

    public User Assignee { get; set; }
}