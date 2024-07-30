using ToDoApp.Data.Models;

namespace ToDoApp.Services.Dtos;

public class UpdateStatusDto
{
    public StatusEnum StatusId { get; set; }
}
