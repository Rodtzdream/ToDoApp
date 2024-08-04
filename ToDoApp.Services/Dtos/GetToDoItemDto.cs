using ToDoApp.Data.Models;

namespace ToDoApp.Services.Dtos;

public class GetToDoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum StatusId { get; set; }
    public int BoardId { get; set; }
    public int AssigneeId { get; set; }
}
