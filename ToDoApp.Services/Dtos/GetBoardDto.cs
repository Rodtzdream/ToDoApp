namespace ToDoApp.Services.Dtos;

public class GetBoardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GetToDoItemDto> ToDoItems { get; set; }
}
