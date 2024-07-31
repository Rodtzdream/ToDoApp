using System.Text.Json.Serialization;

namespace ToDoApp.Data.Models;

public class ToDoItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int BoardId { get; set; }

    [JsonIgnore]
    public Board Board { get; set; }

    public DateTime CreatedAt { get; set; }

    public int StatusId { get; set; }

    public StatusEnum Status
    {
        set => StatusId = (int)value;
    }

    public DateTime DueDate { get; set; }

    public int AssigneeId { get; set; }

    [JsonIgnore]
    public User Assignee { get; set; }
}