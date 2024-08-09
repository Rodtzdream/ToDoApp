using ToDoApp.Data.Models;

namespace ToDoApp.Services.Dtos;

public class UpdateStatusDto
{
    public int Id { get; set; }
    public StatusEnum StatusId { get; set; }
}
