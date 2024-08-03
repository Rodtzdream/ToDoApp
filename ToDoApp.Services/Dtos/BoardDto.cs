namespace ToDoApp.Services.Dtos;

public class BoardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ToDoItemDto> ToDoItems { get; set; }
}
